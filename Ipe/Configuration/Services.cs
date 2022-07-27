using Ipe.External.Services;
using Ipe.UseCases.Interfaces;
using Ipe.UseCases.Interfaces.Services;
using SendGrid;
namespace Ipe.Configuration
{
	public class Services
	{
		public static void Configure(WebApplicationBuilder builder)
        {
			builder.Services.AddSingleton<IAuthService, JWTService>();

			builder.Services.AddSingleton(x =>
				new SendGridClient(builder.Configuration["Email:ApiKey"])
			);

			builder.Services.AddSingleton<IEmailService, SendGridService>();
			builder.Services.AddSingleton<IPaymentService, PaymentService>();
        }
	}
}