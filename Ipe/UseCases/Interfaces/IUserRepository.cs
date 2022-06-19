using Ipe.Domain.Models;
using Ipe.UseCases.Interfaces.Repositories;

namespace Ipe.UseCases.Interfaces
{
	public interface IUserRepository : IBaseRepository<User>
	{
		Task<User> GetByEmail(string Email);
	}
}

