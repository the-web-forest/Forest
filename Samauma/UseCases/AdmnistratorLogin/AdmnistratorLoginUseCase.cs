using Samauma.Domain.Models;
using Samauma.Domain.Errors;
using Samauma.UseCases.Interfaces;
using BCryptLib = BCrypt.Net.BCrypt;
using Samauma.Domain;

namespace Samauma.UseCases.AdmnistratorLogin
{
    public class AdmnistratorLoginUseCase : IUseCase<AdmnistratorLoginUseCaseInput, AdmnistratorLoginUseCaseOutput>
	{

        private readonly IAuthService _authService;
        private readonly IAdmnistratorRepository _admnistratorRepository;

        public AdmnistratorLoginUseCase(
            IAuthService authService,
            IAdmnistratorRepository admnistratorRepository
        )
		{
            _authService = authService;
            _admnistratorRepository = admnistratorRepository;
		}

        public async Task<AdmnistratorLoginUseCaseOutput> Run(AdmnistratorLoginUseCaseInput Input)
        {
            var Admnistrator = await _admnistratorRepository.GetByEmail(Input.Email);

            ValidateAdmnistrator(Admnistrator);

            var passwordIsValid = BCryptLib.Verify(Input.Password, Admnistrator.Password);

            if (passwordIsValid is false)
                throw new InvalidPasswordException();

            return BuildResponse(Admnistrator);
        }

        private static void ValidateAdmnistrator(Admnistrator Admnistrator)
        {
            if (Admnistrator is null)
                throw new InvalidPasswordException();

        }

        private AdmnistratorLoginUseCaseOutput BuildResponse(Admnistrator Admnistrator)
        {
            return new AdmnistratorLoginUseCaseOutput
            {
                AccessToken = _authService.GenerateToken(Admnistrator, Roles.Admin),
                TokenType = "Bearer",
                User = new OutputUser
                {
                    Id = Admnistrator.Id,
                    Email = Admnistrator.Email,
                    Name = Admnistrator.Name
                }
            };
        }

    }
}