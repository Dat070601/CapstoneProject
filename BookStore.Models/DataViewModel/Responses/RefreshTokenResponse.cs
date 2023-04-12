using BookStore.Models.DTOs.Responses.Base;
using BookStore.Models.Entities;

namespace BookStore.Models.DataViewModel.Responses
{
    public class RefreshTokenResponse : GeneralResponses
    {
        public RefreshToken? RefreshToken { get; set; }
    }
}
