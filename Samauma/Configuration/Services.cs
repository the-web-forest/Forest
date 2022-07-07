using Samauma.External.Services;
using Samauma.UseCases.Interfaces;

namespace Samauma.Configuration
{
	public class Services
	{
		public static void Configure(WebApplicationBuilder builder)
        {
			builder.Services.AddScoped<IAuthService, JWTService>();
		}
	}
}