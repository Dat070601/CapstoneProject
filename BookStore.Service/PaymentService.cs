using BookStore.Models.DAL.Interfaces;
using BookStore.Models.DataViewModel.Requests;
using BookStore.Models.DataViewModel.Responses;
using BookStore.Models.Entities;
using BookStore.Service.Base;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service
{
    public class PaymentService : BaseService, IPaymentService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IStatusRepository statusRepository;
        private readonly IConfiguration configuration;
        private readonly IHistoryTransactionsRepository historyTransactionsRepository;
        public PaymentService(
            IUnitOfWork unitOfWork, 
            IMapperCustom mapperCustom,
            IOrderRepository orderRepository, 
            IConfiguration configuration, 
            IHistoryTransactionsRepository repositoryTransactionsRepository,
            IStatusRepository statusRepository) : base(unitOfWork, mapperCustom)
        {
            this.orderRepository = orderRepository;
            this.configuration = configuration;
            this.statusRepository = statusRepository;
            this.historyTransactionsRepository = repositoryTransactionsRepository;
        }

        public async Task<PaymentLinkResponse> CreatePayemntUrl(PaymentRequest paymentRequest, HttpContext context)
        {
            var findOrder = await orderRepository.GetQuery(or => or.Id == paymentRequest.OrderId).SingleAsync();
            if (findOrder == null) return new PaymentLinkResponse { Message = "Không tìm thấy đơn hàng" };
            if (findOrder.Status.NameStatus.Equals("Đã Thanh Toán")) return new PaymentLinkResponse { Message = "Đơn hàng đã thanh toán" };
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(configuration["TimeZoneId"]!);
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var tick = DateTime.Now.Ticks.ToString();
            var pay = new VnPayLibrary();
            var urlCallBack = configuration["PaymentCallBack:ReturnUrl"];

            pay.AddRequestData("vnp_Version", configuration["Vnpay:Version"]!);
            pay.AddRequestData("vnp_Command", configuration["Vnpay:Command"]!);
            pay.AddRequestData("vnp_TmnCode", configuration["Vnpay:TmnCode"]!);
            pay.AddRequestData("vnp_Amount", ((int)findOrder.Total * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", configuration["Vnpay:CurrCode"]!);
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", configuration["Vnpay:Locale"]!);
            pay.AddRequestData("vnp_OrderInfo", $"{paymentRequest.OrderId}");
            pay.AddRequestData("vnp_OrderType", paymentRequest.OrderType!);
            pay.AddRequestData("vnp_ReturnUrl", urlCallBack!);
            pay.AddRequestData("vnp_TxnRef", tick);

            var paymentUrl =
                pay.CreateRequestUrl(configuration["Vnpay:BaseUrl"]!, configuration["Vnpay:HashSecret"]!);

            return new PaymentLinkResponse
            {
                RedirectUrl = paymentUrl,
                IsSuccess = true,
            };
        }

        public async Task<PaymentResponse> PaymentExecute(IQueryCollection collections)
        {
            var pay = new VnPayLibrary();
            var response = pay.GetFullResponseData(collections, configuration["Vnpay:HashSecret"]!);
            if (!response.IsSuccess)
            {
                return response;
            }
            var changeStatus = await statusRepository.FindAsync(st => st.NameStatus.Equals("Đã Thanh Toán"));
            var findOrder = await orderRepository.GetQuery(or => or.Id == new Guid(response.OrderDescription!)).SingleAsync();
            findOrder.StatusId = changeStatus.Id;
            orderRepository.Update(findOrder);
            var history = new HistoryTransaction
            {
                AccountId = findOrder.AccountId,
                StatusId = changeStatus.Id,
                TransactionDate = DateTime.Now,
                Money = Double.Parse(response.Amount!) / 100
            };
            await historyTransactionsRepository.AddAsync(history);
            await unitOfWork.CommitTransaction();
            return response;
        }
    }
}
