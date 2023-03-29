using ErrorOr;
using MediatR;
using ProCardsNew.Application.Account.PasswordRecovery.Common;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Application.Common.Interfaces.Services;
using ProCardsNew.Domain.Common.Errors;

namespace ProCardsNew.Application.Account.PasswordRecovery.Queries.PasswordRecoveryCode;

public class PasswordRecoveryCodeQueryHandler
    : IRequestHandler<PasswordRecoveryCodeQuery, ErrorOr<PasswordRecoveryResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public PasswordRecoveryCodeQueryHandler(
        IUserRepository userRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _dateTimeProvider = dateTimeProvider;
    }
    
    public async Task<ErrorOr<PasswordRecoveryResult>> Handle(PasswordRecoveryCodeQuery query, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetUserByEmailAsync(query.Email.ToUpper()) is not { } user)
            return Errors.User.NotFound;

        if (user.PasswordRecoveryCode == null ||
            user.PasswordRecoveryEndDateTime == null)
            return Errors.User.WrongRecoveryCode;
        
        if (user.PasswordRecoveryEndDateTime < _dateTimeProvider.UtcNow)
            return Errors.User.RecoveryCodeExpired;

        if (user.PasswordRecoveryCode != query.Code)
            return Errors.User.WrongRecoveryCode;

        return new PasswordRecoveryResult();
    }
}