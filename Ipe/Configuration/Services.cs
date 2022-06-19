using Ipe.External.Services;
using Ipe.UseCases.Interfaces;
using SendGrid;
namespace Ipe.Configuration
{
	public class Services
	{
		public static void Configure(WebApplicationBuilder builder)
        {
			builder.Services.AddScoped<IAuthService, JWTService>();

			builder.Services.AddSingleton(x =>
				new SendGridClient(builder.Configuration["Email:ApiKey"])
			);

			builder.Services.AddScoped<IEmailService, SendGridService>();
		}
	}
}