namespace BookStore.Models.DataViewModel
{
    public class ReviewViewModel
    {
        public Guid ReviewId { get; set; }
        public Guid AccountId { get; set; }
        public string? ReviewText { get; set; }
    }
}
