﻿using AutoMapper;
using BookStore.Models.DataViewModel;
using BookStore.Models.Entities;
using BookStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Mapping
{
    public class Mapper : IMapperCustom
    {
        private readonly IMapper autoMapper;
        public Mapper(IMapper autoMapper)
        {
            this.autoMapper = autoMapper;
        }

        public List<AuthorViewModel> MapAuthors(List<Author> authors)
        {
            var listAuthors = new List<AuthorViewModel>();
            foreach (var item in authors)
            {
                var author = new AuthorViewModel
                {
                    AuthorId = item.Id,
                    AuthorName = item.AuthorName,
                };
                listAuthors.Add(author);
            }
            return listAuthors;
        }

        public BookDetailViewModel MapBookDetail(Book bookDetail)
        {
            var book = new BookDetailViewModel()
            {
                Id = bookDetail.Id,
                Title = bookDetail.BookName,
                Author = bookDetail.Author.AuthorName,
                CateegoryId = bookDetail.Category.Id,
                CategoryName = bookDetail.Category.CategoryName,
                DateCreated = bookDetail.DateCreated,
                DefaultPrice = bookDetail.BookPrice.BookDefaultPrice,
                SalePrice = bookDetail.BookPrice.BookSalePrice,
                Description = bookDetail.BookDescription,
                NumPage = bookDetail.NumPage,
                Quantity = bookDetail.Quantity,
                Publisher = bookDetail.Publisher.PublisherName,
                Reviews = MapReview(bookDetail.Reviews.ToList()),
                Sold = bookDetail.Sold,
                Images = MapImages(bookDetail.Images.ToList()),
            };
            return book;
        }

        public List<BookViewModel> MapBookPagging(List<Book> books)
        {
            var listBooks = new List<BookViewModel>();
            foreach (var item in books)
            {
                if(item.BookPrice.ExpirationDate < DateTime.Now)
                {
                    var book = new BookViewModel
                    {
                        Id = item.Id,
                        Author = item.Author.AuthorName,
                        ImageUrl = MapImages(item.Images.ToList())[0].ImageUrl,
                        DefaultPrice = item.BookPrice.BookDefaultPrice,
                        SalePrice = item.BookPrice.BookSalePrice,
                        Quantity = item.Quantity,
                        Price = item.BookPrice.BookDefaultPrice,
                        NameCategory = item.Category.CategoryName,
                        Title = item.BookName,
                        Sold = item.Sold
                    };
                    listBooks.Add(book);
                }
                else
                {
                    var book = new BookViewModel
                    {
                        Id = item.Id,
                        Author = item.Author.AuthorName,
                        ImageUrl = MapImages(item.Images.ToList())[0].ImageUrl,
                        DefaultPrice = item.BookPrice.BookDefaultPrice,
                        SalePrice = item.BookPrice.BookDefaultPrice,
                        Quantity = item.Quantity,
                        Price = item.BookPrice.BookSalePrice,
                        NameCategory = item.Category.CategoryName,
                        Title = item.BookName,
                        Sold = item.Sold
                    };
                    listBooks.Add(book);
                }
            }
            return listBooks;
        }

        public List<ImageViewModel> MapImages(List<Image> images)
        {
            return autoMapper.Map<List<Image>, List<ImageViewModel>>(images);
        }

        public List<OrderViewModel> MapOrder(List<Order> orders)
        {
            var listOrder = new List<OrderViewModel>();
            foreach (var item in orders)
            {
                var order = new OrderViewModel
                {
                    OrderId = item.Id,
                    PaymentMethod = item.Payment.Type,
                    NameCus = item.Account.Name,
                    OrderDate = item.DateCreated,
                    PhoneNumber = item.PhoneNumber,
                    Address = item.Address,
                    City = item.City,
                    District = item.District,
                    TotalPrice = item.Total,
                    Message = item.Message,
                    OrderStatus = item.Status.NameStatus
                };
                listOrder.Add(order);
            }
            return listOrder;
        }

        public List<ReviewViewModel> MapReview(List<Review> reviews)
        {
            var listReview = new List<ReviewViewModel>();
            foreach (var item in reviews)
            {
                var review = new ReviewViewModel
                {
                    AccountId = item.AccountId,
                    ReviewId = item.Id,
                    Name = item.Account.Name,
                    ReviewText = item.ReviewText
                };
                listReview.Add(review);
            }
            return listReview;
        }
    }
}
