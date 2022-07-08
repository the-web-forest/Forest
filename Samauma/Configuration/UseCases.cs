using Samauma.UseCases;
using Samauma.UseCases.AdministratorLogin;

namespace Samauma.Configuration
{
	public class UseCases
	{
		public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUseCase<AdministratorLoginUseCaseInput, AdministratorLoginUseCaseOutput>, AdministratorLoginUseCase>();
        }
    }
}

