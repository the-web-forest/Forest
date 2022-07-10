using Samauma.Domain;
using Samauma.Domain.Models;

namespace Samauma.UseCases.Interfaces
{
	public interface IAuthService
	{
		string GenerateToken(Administrator User, Roles Role);
	}
}
