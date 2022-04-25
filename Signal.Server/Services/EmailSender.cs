using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Signal.Server.Services.Contracts;
using Signal.Server.Services.Options;

namespace Signal.Server.Services;

public class EmailSender : IEmailSender
{
    private readonly ILogger<EmailSender> _logger;
    private readonly SendgridOptions _sendgridOptions;

    public EmailSender(ILogger<EmailSender> logger, IOptions<SendgridOptions> sendgridOptions)
    {
        _logger = logger;
        _sendgridOptions = sendgridOptions.Value;
    }
    

    public async Task<bool> SendEmailAsync(string to, string subject, string htmlMessage)
    {
        var senderAddress = new EmailAddress()
            {Name = _sendgridOptions.SenderName, Email = _sendgridOptions.SenderEmail};
        var msg = GetMessage(senderAddress, subject, htmlMessage);
        msg.AddTo(new EmailAddress(to));
        _logger.LogInformation("Sending email '" + msg.Subject + "'");
        var client = new SendGridClient(_sendgridOptions.ApiKey);
        var response = await client.SendEmailAsync(msg);
        return response.IsSuccessStatusCode;
    }

    private static SendGridMessage GetMessage(EmailAddress emailAddress, string subject, string message)
    {
        return new SendGridMessage
        {
            From = emailAddress,
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
    }
}