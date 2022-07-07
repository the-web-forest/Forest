using Ipe.Domain;
using Ipe.Domain.Errors;
using Ipe.Domain.Models;
using Ipe.UseCases.Interfaces;
using BCryptLib = BCrypt.Net.BCrypt;


namespace Ipe.UseCases.Register
{
	public class UserRegisterUseCase: IUseCase<UserRegisterUseCaseInput, UserRegisterUseCaseOutput>
	{
        private readonly IUserRepository _userRepository;
        private readonly IMailVerificationRepository _mailVerificationRepository;
        private readonly IEmailService _emailService;

        public UserRegisterUseCase(
            IUserRepository userRepository,
            IMailVerificationRepository mailVerificationRepository,
            IEmailService emailService
        )
        {
            _userRepository = userRepository;
            _mailVerificationRepository = mailVerificationRepository;
            _emailService = emailService;
        }

        public async Task<UserRegisterUseCaseOutput> Run(UserRegisterUseCaseInput Input)
        {
            await VerifyIfUserIsAlreadyRegistered(Input.Email);
            await CreateUser(Input);
            var UserRegistrationToken = await CreateMailVerificationRegister(Input);
            await SendWelcomeEmail(Input.Email, Input.Name, UserRegistrationToken);
            return new UserRegisterUseCaseOutput();            
        }

        private async Task VerifyIfUserIsAlreadyRegistered(string Email)
        {
            var UserAlreadyExists = await _userRepository.GetByEmail(Email);

            if (UserAlreadyExists != null)
                throw new EmailAlreadyRegisteredException();
        }

        private async Task CreateUser(UserRegisterUseCaseInput Input)
        {           
            await _userRepository.Create(new User
            {
                Email = Input.Email,
                Name = Input.Name,
                Password = BCryptLib.HashPassword(Input.Password),
                City = Input.City,
                State = Input.State,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                EmailVerified = false
            });
        }

        private async Task<string> CreateMailVerificationRegister(UserRegisterUseCaseInput Input)
        {
            var UserRegistrationToken = Guid.NewGuid().ToString();

            await _mailVerificationRepository.Create(new MailVerification {
                Role = Roles.User.ToString(),
                Email = Input.Email,
                Activated = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
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
}

