using BookStore.Models.DataViewModel;
using BookStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Interfaces
{
    public interface IMapperCustom
    {
        List<BookViewModel> MapBookPagging(List<Book> books);
        List<ImageViewModel> MapImages(List<Image> images);
        List<ReviewViewModel> MapReview(List<Review> reviews);
        BookDetailViewModel MapBookDetail(Book bookDetail);
    }
}
