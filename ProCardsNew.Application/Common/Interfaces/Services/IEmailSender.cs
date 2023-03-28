using ProCardsNew.Application.Common.Enums;

namespace ProCardsNew.Application.Common.Interfaces.Services;

public interface IEmailSender
{
    public Task<EmailResult> SendEmailAsync(
        string emailAddress,
        string message,
        string subject);
}