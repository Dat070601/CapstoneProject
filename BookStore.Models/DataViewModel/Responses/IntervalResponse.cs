using BookStore.Models.DTOs.Responses.Base;

namespace BookStore.Models.DataViewModel.Responses
{
    public class IntervalResponse : GeneralResponses
    {
        public string? Color { get; set; }
        public string? Unit { get; set; }
        public string? Title { get; set; }
        public List<string>? Labels { get; set; }
        public List<int>? Data { get; set; }
    }
}
