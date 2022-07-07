using Ipe.Domain;
using Ipe.Domain.Models;

namespace Ipe.UseCases.Interfaces
{
	public interface IAuthService
	{
		string GenerateToken(User User, Roles Role);
	}
}
