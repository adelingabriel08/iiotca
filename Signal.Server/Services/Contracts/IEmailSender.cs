namespace Signal.Server.Services.Contracts;

public interface IEmailSender
{
    Task<bool> SendEmailAsync(string to, string subject, string htmlMessage);
}