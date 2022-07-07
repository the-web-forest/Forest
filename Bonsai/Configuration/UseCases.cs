using Bonsai.UseCases;
using Bonsai.UseCases.NewPaymentUseCase;

namespace Bonsai.Configuration
{
	public class UseCases
	{
		public static void Configure(WebApplicationBuilder builder)
        {
			builder.Services.AddScoped<IUseCase<NewPaymentUseCaseInput, NewPaymentUseCaseOutput>, NewPaymentUseCase>();
		}
	}
}

