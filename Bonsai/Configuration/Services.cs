using Bonsai.External.Services.Payment.Pagseguro;
using Bonsai.UseCases.Interfaces.Services;

namespace Bonsai.Configuration
{
	public class Services
	{
		public static void Configure(WebApplicationBuilder builder)
        {
			builder.Services.AddScoped<IPaymentService, PagseguroService>();
		}
	}
}