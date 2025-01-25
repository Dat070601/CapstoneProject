using AutoMapper;
using BookStore.Models.DataViewModel;
using BookStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<Book, BookViewModel>();
            CreateMap<Image, ImageViewModel>();
            CreateMap<Book, BookDetailViewModel>();
            CreateMap<Review, ReviewViewModel>();
            CreateMap<Author, AuthorViewModel>();
            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderDetail, OrderDetailViewModel>();
        }
    }
}
