using Samauma.Domain.Models;
using Samauma.UseCases.Interfaces.Repositories;

namespace Samauma.UseCases.Interfaces
{
	public interface IUserRepository : IBaseRepository<User>
	{
		Task<User> GetByEmail(string Email);
		Task<User> GetById(string UserId);
		Task<List<User>> ListUsersPerPage(int Page, int ItensPerPage);
		Task<long> CountUsers();
	}
}

