using BookStore.Models.DTOs.Responses.Base;

namespace BookStore.Models.DataViewModel
{
    public class BookDetailViewModel : GeneralResponses
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public double DefaultPrice { get; set; }
        public double SalePrice { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public List<ImageViewModel>? Images { get; set; }
        public Guid CateegoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public List<ReviewViewModel>? Reviews { get; set; }
        public int Sold { get; set; }
        public int NumPage { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
