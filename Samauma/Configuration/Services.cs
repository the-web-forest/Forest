using Samauma.External.Services;
using Samauma.UseCases.Interfaces;
using Samauma.UseCases.Interfaces.Services;

namespace Samauma.Configuration
{
	public class Services
	{
		public static void Configure(WebApplicationBuilder builder)
        {
			builder.Services.AddScoped<IAuthService, JWTService>();
			builder.Services.AddScoped<IStorageService, StorageService>();
		}
	}
}