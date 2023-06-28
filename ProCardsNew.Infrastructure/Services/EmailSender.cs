using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using ProCardsNew.Application.Common.Enums;
using ProCardsNew.Application.Common.Interfaces.Services;
using ProCardsNew.Infrastructure.Settings;

namespace ProCardsNew.Infrastructure.Services;

public class EmailSender: IEmailSender
{
    private readonly ILogger<EmailSender> _logger;
    private readonly EmailSettings _emailSettings;

    public EmailSender(
        IOptions<EmailSettings> emailSettings,
        ILogger<EmailSender> logger)
    {
        _logger = logger;
        _emailSettings = emailSettings.Value;
    }
    
    public async Task<EmailResult> SendEmailAsync(
        string emailAddress,
        string message,
        string subject)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_emailSettings.From));
        email.To.Add(MailboxAddress.Parse(emailAddress));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Plain) { Text = message };

        try
        {
            using var smtp = new SmtpClient();
            _logger.Log(LogLevel.Information ,"Connecting to smtp");
            await smtp.ConnectAsync(_emailSettings.EmailServiceUrl, 587, SecureSocketOptions.StartTls);
            _logger.Log(LogLevel.Information ,"Authenticating to smtp");
            await smtp.AuthenticateAsync(_emailSettings.From, _emailSettings.Password);
            _logger.Log(LogLevel.Information ,"Sending email to smtp");
            await smtp.SendAsync(email);
            _logger.Log(LogLevel.Information ,"Disconnecting from smtp");
            await smtp.DisconnectAsync(true);

            return EmailResult.Success;
        }
        catch
        {
            return EmailResult.Failure;
        }
        finally
        {
            email.Dispose();
        }
    }
}