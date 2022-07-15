using Ipe.Domain.Models;
using Ipe.Domain.Errors;
using Ipe.UseCases.Interfaces;
using BCryptLib = BCrypt.Net.BCrypt;
using Ipe.Domain;

namespace Ipe.UseCases.Login
{
    public class LoginUseCase : IUseCase<LoginUseCaseInput, LoginUseCaseOutput>
	{

        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public LoginUseCase(
            IAuthService authService,
            IUserRepository userRepository
        )
		{
            _authService = authService;
            _userRepository = userRepository;
		}

        public async Task<LoginUseCaseOutput> Run(LoginUseCaseInput Input)
        {
            var User = await _userRepository.GetByEmail(Input.Email);

            ValidateUser(User);

            var passwordIsValid = BCryptLib.Verify(Input.Password, User.Password);

            if (passwordIsValid is false)
                throw new InvalidPasswordException();

            return BuildResponse(User);
        }

        private static void ValidateUser(User User)
        {
            if (User is null)
                throw new InvalidPasswordException();

            if (User.EmailVerified is false)
                throw new UnverifiedEmailException();
        }

        private LoginUseCaseOutput BuildResponse(User User)
        {
            return new LoginUseCaseOutput
            {
                AccessToken = _authService.GenerateToken(User, Roles.User),
                TokenType = "Bearer",
                User = new OutputUser
                {
                    Id = User.Id,
                    Email = User.Email,
                    Name = User.Name
                }
            };
        }

    }
}