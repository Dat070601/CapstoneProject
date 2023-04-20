using BookStore.Models.DAL.Interfaces;
using BookStore.Models.DataViewModel;
using BookStore.Models.DataViewModel.Requests;
using BookStore.Models.DataViewModel.Responses;
using BookStore.Models.Entities;
using BookStore.Service.Base;
using BookStore.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IBookPriceRepository bookPriceRepository;
        private readonly IOrderDetailRepository orderDetailRepository;
        private readonly IBookRepository bookRepository;
        private readonly ICartRepository cartRepository;
        private readonly ICartDetailRepository cartDetailRepository;
        private readonly IStatusRepository statusRepository;
        public OrderService(
            IUnitOfWork unitOfWork, 
            IMapperCustom mapperCustom,
            IOrderRepository orderRepository,
            IBookPriceRepository bookPriceRepository,
            IOrderDetailRepository orderDetailRepository,
            ICartRepository cartRepository,
            ICartDetailRepository cartDetailRepository,
            IBookRepository bookRepository,
            IStatusRepository statusRepository) : base(unitOfWork, mapperCustom)
        {
            this.orderRepository = orderRepository;
            this.orderDetailRepository = orderDetailRepository;
            this.bookPriceRepository = bookPriceRepository;
            this.bookRepository = bookRepository;
            this.cartDetailRepository = cartDetailRepository;
            this.cartRepository = cartRepository;
            this.statusRepository = statusRepository;
        }

        public async Task<OrderResponse> ChangeStatus(StatusRequest status, Guid orderId)
        {
            try
            {
                var order = await orderRepository.GetQuery(or => or.Id == orderId).SingleAsync();
                var findStatus = await statusRepository.GetQuery(st => st.NameStatus.Equals(status.StatusName)).SingleAsync();
                if(status.StatusName!.Equals("Đã Hủy"))
                {
                    order.StatusId = findStatus.Id;
                    var orderDetail = await orderDetailRepository.GetQuery(ord => ord.OrderId == orderId).ToListAsync();
                    foreach (var item in orderDetail)
                    {
                        var findBook = await bookRepository.GetQuery(b => b.Id == item.BookId).SingleAsync();
                        findBook.Quantity += item.Quantity;
                        findBook.Sold -= item.Quantity;
                        bookRepository.Update(findBook);
                    }
                }
                else
                {
                    order.StatusId = findStatus.Id;
                    orderRepository.Update(order);
                }
                await unitOfWork.CommitTransaction();
                return new OrderResponse
                {
                    IsSuccess = true,
                    Message = "Change status order is success !!"
                };
            }
            catch (NullReferenceException)
            {
                return new OrderResponse
                {
                    IsSuccess = false,
                    Message = "Some properties is Null!"
                };
            }
            catch (Exception)
            {
                return new OrderResponse
                {
                    IsSuccess = false,
                    Message = "System error, please try again later!"
                };
            }
        }

        public async Task<OrderResponse> AddOrder(OrderRequest orderRequest, Guid cusId)
        {
            try
            {
                var order = await createOrder(orderRequest, cusId);
                var req = await createOrderDetail(orderRequest, order);
                if (!req.IsSuccess) return req;
                await deleteBookInCart(order, orderRequest);
                await unitOfWork.CommitTransaction();
                return new OrderResponse
                {
                    IsSuccess = true,
                    Message = "Add Order Success!!"
                };
            }
            catch (InvalidOperationException)
            {
                return new OrderResponse
                {
                    IsSuccess = false,
                    Message = "Some properties is valid !",
                };
            }
            catch (Exception)
            {
                return new OrderResponse
                {
                    IsSuccess = false,
                    Message = "System error, please try again later!"
                };
            }
        }

        public async Task<OrderViewModel> GetOrder(Guid orderId)
        {
            var order = await orderRepository.GetQuery(or => or.Id == orderId).SingleAsync();
            var orderView = new OrderViewModel
            {
                OrderId = order.Id,
                CustomerId = order.AccountId,
                Address = order.Address,
                City = order.City,
                Country = order.Country,
                MessageOrder = order.Message,
                OrderDate = order.DateCreated,
                PhoneNumber = order.PhoneNumber,
                OrderStatus = order.Status.NameStatus,
                PaymentMethod = order.Payment.Type,
                TotalPrice = order.Total,
            };
            var orderDetail = await orderDetailRepository.GetQuery(ord => ord.OrderId == order.Id).ToListAsync();
            var listOrderDetail = new List<OrderDetailViewModel>();
            foreach (var item in orderDetail)
            {
                var ord = new OrderDetailViewModel
                {
                    BookId = item.BookId,
                    BookName = item.Book.BookName,
                    Price = item.Price,
                    Quantity = item.Quantity
                };
                listOrderDetail.Add(ord);
            }
            orderView.OrderDetails = listOrderDetail;
            return orderView;
        }

        private async Task<Order> createOrder(OrderRequest orderRequest, Guid cusId)
        {
            var order = new Order
            {
                AccountId = cusId,
                Address = orderRequest.Address,
                City = orderRequest.City,
                Country = orderRequest.Country,
                PaymentId = orderRequest.PaymentId,
                PhoneNumber = orderRequest.PhoneNumber,
                StatusId = new Guid("1BB6C8CF-F16E-4153-AB89-50534E3710A4"),
                DateCreated = DateTime.Now,
                Total = 0
            };
            await orderRepository.AddAsync(order);
            return order;
        }

        private async Task<OrderResponse> createOrderDetail(OrderRequest orderReq, Order order)
        {
            foreach (var item in orderReq.OrderDetails!)
            {
                if (item.Quantity == 0)
                {
                    return new OrderResponse
                    {
                        IsSuccess = false,
                        Message = "Can't order quantity less than 0!"
                    };
                }
                var price = await bookPriceRepository.GetPriceByBookId(item.BookId);
                var orderDetail = new OrderDetail
                {
                    BookId = item.BookId,
                    OrderId = order.Id,
                    Quantity = item.Quantity,
                    Price = price * item.Quantity,
                };
                await orderDetailRepository.AddAsync(orderDetail);
                order.Total += orderDetail.Price;
                var book = await bookRepository.GetQuery(b => b.Id == item.BookId).SingleAsync();
                book.Quantity -= item.Quantity;
                book.Sold += item.Quantity;
                if (book.Quantity < 0)
                {
                    return new OrderResponse
                    {
                        IsSuccess = false,
                        Message = "Can't order more than available quantity!"
                    };
                }
                bookRepository.Update(book);
            }
            return new OrderResponse
            {
                IsSuccess = true,
            };
        }

        private async Task deleteBookInCart(Order order, OrderRequest orderReq)
        {
            foreach (var item in orderReq.OrderDetails!)
            {
                var findCart = await cartRepository.GetQuery(o => o.AccountId == order.AccountId).SingleAsync();
                var findCartDetail = await cartDetailRepository.GetCartDetailByCartIdAndBookId(findCart.Id, item.BookId);
                if (findCartDetail != null) cartDetailRepository.Delete(findCartDetail);
            }
        }
    }
}
