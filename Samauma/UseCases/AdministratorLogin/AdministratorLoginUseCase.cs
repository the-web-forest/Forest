using Samauma.Domain.Models;
using Samauma.Domain.Errors;
using Samauma.UseCases.Interfaces;
using BCryptLib = BCrypt.Net.BCrypt;
using Samauma.Domain;

namespace Samauma.UseCases.AdministratorLogin
{
    public class AdministratorLoginUseCase : IUseCase<AdministratorLoginUseCaseInput, AdministratorLoginUseCaseOutput>
	{

        private readonly IAuthService _authService;
        private readonly IAdministratorRepository _admnistratorRepository;

        public AdministratorLoginUseCase(
            IAuthService authService,
            IAdministratorRepository admnistratorRepository
        )
		{
            _authService = authService;
            _admnistratorRepository = admnistratorRepository;
		}

        public async Task<AdministratorLoginUseCaseOutput> Run(AdministratorLoginUseCaseInput Input)
        {
            var Administrator = await _admnistratorRepository.GetByEmail(Input.Email);

            ValidateAdministrator(Administrator);

            var passwordIsValid = BCryptLib.Verify(Input.Password, Administrator.Password);

            if (passwordIsValid is false)
                throw new InvalidPasswordException();

            return BuildResponse(Administrator);
        }

        private static void ValidateAdministrator(Administrator Admnistrator)
        {
            if (Admnistrator is null)
                throw new InvalidPasswordException();

        }

        private AdministratorLoginUseCaseOutput BuildResponse(Administrator Administrator)
        {
            return new AdministratorLoginUseCaseOutput
            {
                AccessToken = _authService.GenerateToken(Administrator, Roles.Admin),
                TokenType = "Bearer",
                User = new OutputUser
                {
                    Id = Administrator.Id,
                    Email = Administrator.Email,
                    Name = Administrator.Name
                }
            };
        }

    }
}