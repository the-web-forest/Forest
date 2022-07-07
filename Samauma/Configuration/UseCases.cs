using Samauma.UseCases;
using Samauma.UseCases.AdmnistratorLogin;

namespace Samauma.Configuration
{
	public class UseCases
	{
		public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUseCase<AdmnistratorLoginUseCaseInput, AdmnistratorLoginUseCaseOutput>, AdmnistratorLoginUseCase>();
        }
    }
}

