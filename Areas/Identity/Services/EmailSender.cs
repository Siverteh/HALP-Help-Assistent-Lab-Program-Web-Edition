using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace OperationCHAN.Areas.Identity.Services;

public class EmailSender : IEmailSender
{
    private readonly ILogger _logger;

    public EmailSender(ILogger<EmailSender> logger)
    {
        _logger = logger;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        var client = new SendGridClient("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");
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