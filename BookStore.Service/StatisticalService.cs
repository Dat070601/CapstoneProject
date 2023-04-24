using BookStore.Models.DAL.Interfaces;
using BookStore.Models.DataViewModel.Responses;
using BookStore.Models.Entities;
using BookStore.Service.Base;
using BookStore.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<StatisResponse> NumberOfBooksSold(string type)
        {
            var dateTime = DateTime.Now;
            var labels1 = new StatisResponse();
            switch (type)
            {
                case "7Days":
                {
                    for(int i = 1; i < 8; i++)
                    {
                        var date = dateTime.AddDays(-i);
                        var listOrderInMonth = new List<Order>();
                        if(date.Day >=10)
                        {
                            listOrderInMonth = await orderRepository.GetOrderByDate(date.Year.ToString() + "-" + date.Month.ToString() + "-" + date.Day.ToString());
                            foreach (var item in listOrderInMonth)
                            {
                                var listOrderDetail = await orderDetailRepository.GetQuery(ord => ord.OrderId == item.Id).ToListAsync();
                                var listBook = new List<StatisBookResponse>();
                                foreach (var orderDetail in listOrderDetail)
                                {
                                    var book = new StatisBookResponse
                                    {
                                        BookName = orderDetail.Book.BookName,
                                        Count = orderDetail.Quantity
                                    };
                                    //if(listBook.)
                                    listBook.Add(book);
                                    
                                }
                            }
                        }
                    }
                    break;
                }
                default:
                    break;
            }
            throw new NotImplementedException();
        }
    }
}
