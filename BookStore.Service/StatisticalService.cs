using BookStore.Models.DAL.Interfaces;
using BookStore.Models.DataViewModel.Responses;
using BookStore.Models.Entities;
using BookStore.Service.Base;
using BookStore.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System;
using System.Drawing;

namespace BookStore.Service
{
    public class StatisticalService : BaseService, IStatisticalService
    {
        private readonly IBookRepository bookRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderDetailRepository orderDetailRepository;
        public StatisticalService(
            IUnitOfWork unitOfWork, 
            IMapperCustom mapperCustom,
            IBookRepository bookRepository,
            IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository) : base(unitOfWork, mapperCustom)
        {
            this.bookRepository = bookRepository;
            this.orderRepository = orderRepository;
            this.orderDetailRepository = orderDetailRepository;
        }

        private async Task<StatisResponse> numberBookSold(int number, string title, string color, string unit, DateTime dateTime)
        {
            var labels1 = new StatisResponse();
            var orderDetails = new List<OrderDetail>();
            var labels = new List<string>();
            var data = new List<int>();
            for (int i = 1; i <= number; i++)
            {
                var date = dateTime.AddDays(-i);
                var listOrderInMonth = new List<Order>();
                var listOrderDetail7Day = new List<OrderDetail>();
                listOrderInMonth = await orderRepository.GetOrderByDate(date);
                if (listOrderInMonth.Count != 0)
                {
                    foreach (var item in listOrderInMonth)
                    {
                        var listOrderDetail = await orderDetailRepository.GetQuery(ord => ord.OrderId == item.Id).ToListAsync();
                        orderDetails.AddRange(listOrderDetail);
                    }
                }
            }
            var order = orderDetails.GroupBy(g => g.BookId).Select(o => new DateResponse { BookName = o.Key.ToString(), Data = o.Sum(g => g.Quantity) })
                        .OrderByDescending(o => o.Data).Take(5).ToList();
            foreach (var item in order)
            {
                var book = await bookRepository.GetQuery(b => b.Id.ToString().Equals(item.BookName)).SingleAsync();
                labels.Add(book.BookName!);
                data.Add(item.Data);
            }
            labels1.Unit = unit;
            labels1.IsSuccess = true;
            labels1.Title = title;
            labels1.Color = color;
            labels1.Labels = labels;
            labels1.Data = data;
            return labels1;
        }
          
        public async Task<StatisResponse> NumberOfBooksSold(int countDate)
        {
            var dateTime = DateTime.Now;
            return await numberBookSold(countDate, "Năm sản phẩm bán chạy nhất trong " + dateTime.ToString() + " ngày", "", "", dateTime);
        }

        public async Task<StatisResponse> NumberOfBookSoldInMonth(int month, Guid accountId)
        {
            var labels1 = new StatisResponse();
            var orderDetails = new List<OrderDetail>();
            var labels = new List<string>();
            var data = new List<int>();
            var listOrder = await orderRepository.GetOrderInMonth(month);
            foreach (var item in listOrder)
            {
                var listOrderDetail = await orderDetailRepository.GetQuery(ord => ord.OrderId == item.Id).ToListAsync();
                orderDetails.AddRange(listOrderDetail);
            }
            var order = orderDetails.GroupBy(g => g.BookId).Select(o => new DateResponse { BookName = o.Key.ToString(), Data = o.Sum(g => g.Quantity) })
                                   .OrderByDescending(o => o.Data).Take(5).ToList();
            foreach (var item in order)
            {
                var book = await bookRepository.GetQuery(b => b.Id.ToString().Equals(item.BookName)).SingleAsync();
                labels.Add(book.BookName!);
                data.Add(item.Data);
            }
            labels1.Labels = labels;
            labels1.Data = data;
            return labels1;
        }
    }
}
