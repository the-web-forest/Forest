using Ipe.External.Repositories;
using Ipe.UseCases.Interfaces;
using Ipe.UseCases.Interfaces.Repositories;

namespace Ipe.Configuration;

public class Repositories
{
	public static void Configure(WebApplicationBuilder builder) {
		builder.Services.AddScoped<IUserRepository, UserRepository>();
		builder.Services.AddScoped<IMailVerificationRepository, MailVerificationRepository>();
		builder.Services.AddScoped<IStateRepository, StateRepository>();
		builder.Services.AddScoped<IPasswordResetRepository, PasswordResetRepository>();

        #region Plant
        builder.Services.AddScoped<ITreeRepository, TreeRepository>();
		builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IPlantRepository, PlantRepository>();
        #endregion
    }
}

