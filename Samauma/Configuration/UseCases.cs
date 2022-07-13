using Samauma.UseCases;
using Samauma.UseCases.AdministratorLogin;
using Samauma.UseCases.ListUsers;
using Samauma.UseCases.ListTrees;
using Samauma.UseCases.CreateTree;
using Samauma.UseCases.UserDetail;

namespace Samauma.Configuration
{
	public class UseCases
	{
		public static void Configure(WebApplicationBuilder builder)
        {
            #region User
            builder.Services.AddScoped<IUseCase<AdministratorLoginUseCaseInput, AdministratorLoginUseCaseOutput>, AdministratorLoginUseCase>();
            builder.Services.AddScoped<IUseCase<ListUsersUseCaseInput, ListUsersUseCaseOutput>, ListUsersUseCase>();
            builder.Services.AddScoped<IUseCase<UserDetailUseCaseInput, UserDetailUseCaseOutput>, UserDetailUseCase>();
            #endregion

            #region Tree
            builder.Services.AddScoped<IUseCase<CreateTreeUseCaseInput, CreateTreeUseCaseOutput>, CreateTreeUseCase>();
            builder.Services.AddScoped<IUseCase<ListTreesUseCaseInput, ListTreesUseCaseOutput>, ListTreesUseCase>();
            #endregion

        }
    }
}

