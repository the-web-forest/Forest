using Samauma.UseCases;
using Samauma.UseCases.AdministratorLogin;
using Samauma.UseCases.ListUsers;
using Samauma.UseCases.ListTrees;
using Samauma.UseCases.CreateTree;

namespace Samauma.Configuration
{
	public class UseCases
	{
		public static void Configure(WebApplicationBuilder builder)
        {
            #region UserUseCase
            builder.Services.AddScoped<IUseCase<AdministratorLoginUseCaseInput, AdministratorLoginUseCaseOutput>, AdministratorLoginUseCase>();
            builder.Services.AddScoped<IUseCase<ListUsersUseCaseInput, ListUsersUseCaseOutput>, ListUsersUseCase>();
            #endregion

            #region TreeUseCase
            builder.Services.AddScoped<IUseCase<CreateTreeUseCaseInput, CreateTreeUseCaseOutput>, CreateTreeUseCase>();
            builder.Services.AddScoped<IUseCase<ListTreesUseCaseInput, ListTreesUseCaseOutput>, ListTreesUseCase>();
            #endregion
        }
    }
}

