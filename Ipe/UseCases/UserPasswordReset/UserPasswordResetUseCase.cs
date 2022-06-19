using Ipe.Domain;
using Ipe.Domain.Errors;
using Ipe.Domain.Models;
using Ipe.UseCases.Interfaces;

namespace Ipe.UseCases.UserPasswordReset;

public class UserPasswordResetUseCase: IUseCase<UserPasswordResetUseCaseInput, UserPasswordResetUseCaseOutput>
{

    private readonly IUserRepository _userRepository;
    private readonly IPasswordResetRepository _passwordResetRepository;
    private readonly IEmailService _emailService;

    public UserPasswordResetUseCase(
        IUserRepository userRepository,
        IEmailService emailService,
        IPasswordResetRepository passwordResetRepository
    )
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _passwordResetRepository = passwordResetRepository;
    }

    public async Task<UserPasswordResetUseCaseOutput> Run(UserPasswordResetUseCaseInput Input)
	{
        var User = await _userRepository.GetByEmail(Input.Email);

        if (User is null)
            throw new InvalidEmailException();

        var PasswordResetToken = await CreateMailPasswordResetRegisterAsync(Input.Email);

        await SendPasswordResetEmail(Input.Email, User.Name, PasswordResetToken);

        return new UserPasswordResetUseCaseOutput
        {
            Send = true
        };
    }

    private async Task<string> CreateMailPasswordResetRegisterAsync(string Email)
    {
        var UserPasswordReset = Guid.NewGuid().ToString();

        await _passwordResetRepository.Create(new PasswordReset
        {
            Role = Roles.User.ToString(),
            Email = Email,
            Reseted = false,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            ResetedAt = null,
            Token = UserPasswordReset
        });

        return UserPasswordReset;
    }

    private async Task<bool> SendPasswordResetEmail(string Email, string Name, string Token)
    {
        return await _emailService.SendPasswordResetEmail(Email, Name, Token, Roles.User.ToString());
    }

}

