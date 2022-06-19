using Ipe.UseCases;
using Ipe.UseCases.CheckEmail;
using Ipe.UseCases.GetCitiesByState;
using Ipe.UseCases.GetStates;
using Ipe.UseCases.Login;
using Ipe.UseCases.Register;
using Ipe.UseCases.SendVerificationEmail;
using Ipe.UseCases.UserPasswordChange;
using Ipe.UseCases.UserPasswordReset;
using Ipe.UseCases.ValidateEmail;

namespace Ipe.Configuration
{
	public class UseCases
	{
		public static void Configure(WebApplicationBuilder builder)
        {
			builder.Services.AddScoped<IUseCase<LoginUseCaseInput, LoginUseCaseOutput>, LoginUseCase>();
			builder.Services.AddScoped<IUseCase<UserRegisterUseCaseInput, UserRegisterUseCaseOutput>, UserRegisterUseCase>();
			builder.Services.AddScoped<IUseCase<ValidateEmailUseCaseInput, ValidateEmailUseCaseOutput>, ValidateEmailUseCase>();
			builder.Services.AddScoped<IUseCase<CheckEmailUseCaseInput, CheckEmailUseCaseOutput>, CheckEmailUseCase>();
			builder.Services.AddScoped<IUseCase<GetStatesUseCaseInput, GetStatesUseCaseOutput>, GetStatesUseCase>();
			builder.Services.AddScoped<IUseCase<GetCitiesByStateUseCaseInput, GetCitiesByStateUseCaseOutput>, GetCitiesByStateUseCase>();
			builder.Services.AddScoped<IUseCase<SendVerificationEmailUseCaseInput, SendVerificationEmailUseCaseOutput>, SendVerificationEmailUseCase>();
			builder.Services.AddScoped<IUseCase<UserPasswordResetUseCaseInput, UserPasswordResetUseCaseOutput>, UserPasswordResetUseCase>();
			builder.Services.AddScoped<IUseCase<UserPasswordChangeUseCaseInput, UserPasswordChangeUseCaseOutput>, UserPasswordChangeUseCase>();
		}
	}
}

