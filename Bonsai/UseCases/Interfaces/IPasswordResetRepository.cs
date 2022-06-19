using Ipe.Domain.Models;
using Ipe.UseCases.Interfaces.Repositories;

namespace Ipe.UseCases.Interfaces
{
	public interface IPasswordResetRepository : IBaseRepository<PasswordReset>
	{
		PasswordReset GetLastRegisterByEmail(string Email);
	}
}

