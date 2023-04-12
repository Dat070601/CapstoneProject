using BookStore.Models.DTOs.Responses.Base;

namespace BookStore.Models.DataViewModel.Responses
{
    public class TokenResponse : GeneralResponses
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public Guid ShopId { get; set; }
        public int Wallet { get; set; }
    }
}
