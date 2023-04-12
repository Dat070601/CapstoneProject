namespace BookStore.Service.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailVerificationAsync(string toEmail, string code, string emailFor);
    }
}
