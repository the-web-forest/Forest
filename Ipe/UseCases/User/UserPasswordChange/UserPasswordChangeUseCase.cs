using Ipe.Domain.Errors;
using Ipe.Domain.Models;
using Ipe.UseCases.Interfaces;
using BCryptLib = BCrypt.Net.BCrypt;

namespace Ipe.UseCases.UserPasswordChange;

public class UserPasswordChangeUseCase: IUseCase<UserPasswordChangeUseCaseInput, UserPasswordChangeUseCaseOutput>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordResetRepository _passwordResetRepository;
    private readonly IEmailService _emailService;

    public UserPasswordChangeUseCase(
        IUserRepository userRepository,
        IEmailService emailService,
        IPasswordResetRepository passwordResetRepository
    )
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _passwordResetRepository = passwordResetRepository;
    }

    public async Task<UserPasswordChangeUseCaseOutput> Run(UserPasswordChangeUseCaseInput Input)
	{
        var User = await _userRepository.GetByEmail(Input.Email);

        if (User is null)
            throw new InvalidEmailException();

        var LastRegister = _passwordResetRepository.GetLastRegisterByEmail(Input.Email);
        ValidateMailPasswordResetAttempt(LastRegister, Input);
        await UpdatePasswordResetRegister(LastRegister);
        await UpdateUserPasssowrd(User, Input);

        return new UserPasswordChangeUseCaseOutput { Changed = true };
    }

    private static void ValidateMailPasswordResetAttempt(PasswordReset PasswordReset, UserPasswordChangeUseCaseInput Input)
    {
        if (PasswordReset == null)
            throw new InvalidPasswordResetException();

        if (PasswordReset.Reseted == true)
            throw new InvalidPasswordResetException();

        if (PasswordReset.Token != Input.Token)
            throw new InvalidPasswordResetException();

        DateTime Now = DateTime.Now;
        DateTime StartTime = PasswordReset.CreatedAt;
        DateTime EndTime = StartTime.AddHours(24);

        if (Now > EndTime)
            throw new InvalidPasswordResetException();

    }

    private async Task UpdatePasswordResetRegister(PasswordReset PasswordReset)
    {
        PasswordReset.Reseted = true;
        await _passwordResetRepository.Update(PasswordReset);
    }

    private async Task UpdateUserPasssowrd(User User, UserPasswordChangeUseCaseInput Input)
    {
        User.Password = BCryptLib.HashPassword(Input.Password);
        await _userRepository.Update(User);
    }
}

