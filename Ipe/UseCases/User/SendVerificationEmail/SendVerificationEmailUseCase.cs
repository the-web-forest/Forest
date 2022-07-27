using Ipe.Domain;
using Ipe.Domain.Errors;
using Ipe.Domain.Models;
using Ipe.UseCases.Interfaces;

namespace Ipe.UseCases.SendVerificationEmail;

public class SendVerificationEmailUseCase: IUseCase<SendVerificationEmailUseCaseInput, SendVerificationEmailUseCaseOutput>
{
	private readonly IUserRepository _userRepository;
	private readonly IMailVerificationRepository _mailVerificationRepository;
	private readonly IEmailService _emailService;

    public SendVerificationEmailUseCase(
           IUserRepository userRepository,
           IMailVerificationRepository mailVerificationRepository,
           IEmailService emailService
       )
    {
        _userRepository = userRepository;
        _mailVerificationRepository = mailVerificationRepository;
        _emailService = emailService;
    }

    public async Task<SendVerificationEmailUseCaseOutput> Run(SendVerificationEmailUseCaseInput Input)
	{
        var User = await _userRepository.GetByEmail(Input.Email);

        if (User is null)
            throw new InvalidEmailValidationException();

        if(User.EmailVerified is true)
            throw new InvalidEmailValidationException();

        var UserRegistrationToken = await CreateMailVerificationRegister(Input.Email);
        await SendWelcomeEmail(User.Email, User.Name, UserRegistrationToken);

        return new SendVerificationEmailUseCaseOutput { Send = true };
    }


    private async Task<string> CreateMailVerificationRegister(string Email)
    {
        var UserRegistrationToken = Guid.NewGuid().ToString();

        await _mailVerificationRepository.Create(new MailVerification
        {
            Role = Roles.User.ToString(),
            Email = Email,
            Activated = false,
            ActivatedAt = null,
            Token = UserRegistrationToken
        });

        return UserRegistrationToken;
    }

    private async Task<bool> SendWelcomeEmail(string Email, string Name, string Token)
    {
        return await _emailService.SendWelcomeEmail(Email, Name, Token, Roles.User.ToString());
    }
}

