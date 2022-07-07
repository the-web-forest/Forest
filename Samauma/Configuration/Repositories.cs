using Samauma.External.Repositories;
using Samauma.UseCases.Interfaces;

namespace Samauma.Configuration;

public class Repositories
{
	public static void Configure(WebApplicationBuilder builder) {
		builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IAdmnistratorRepository, AdmnistratorRepository>();
	}
}

