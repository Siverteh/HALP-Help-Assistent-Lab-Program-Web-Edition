using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace OperationCHAN.Areas.Identity.Services;

public class EmailSender : IEmailSender
{
    private readonly ILogger _logger;

    public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor, ILogger<EmailSender> logger)
    {
        Options = optionsAccessor.Value;
        _logger = logger;
    }
    public AuthMessageSenderOptions Options { get; }
    
    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        if (string.IsNullOrEmpty(Options.SendGridKey))
        {
            throw new Exception("Null SendGridKey");
        }

        var client = new SendGridClient(Options.SendGridKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress("studassprogram@gmail.com", "Studass program"),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        msg.AddTo(new EmailAddress(toEmail));
        
        msg.SetClickTracking(false, false);
        var response = await client.SendEmailAsync(msg);
        
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        _logger.LogInformation(response.IsSuccessStatusCode
            ? $"Email to {toEmail} queued successfully!"
            : $"Failure Email to {toEmail}");
    }
}
    