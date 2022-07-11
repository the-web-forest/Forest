using Ipe.External.Repositories;
using Ipe.UseCases.Interfaces;

namespace Ipe.Configuration;

public class Repositories
{
	public static void Configure(WebApplicationBuilder builder) {
		builder.Services.AddScoped<IUserRepository, UserRepository>();
		builder.Services.AddScoped<IMailVerificationRepository, MailVerificationRepository>();
		builder.Services.AddScoped<IStateRepository, StateRepository>();
		builder.Services.AddScoped<IPasswordResetRepository, PasswordResetRepository>();
		builder.Services.AddScoped<ITreeRepository, TreeRepository>();
	}
}

