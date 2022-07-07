using Ipe.Domain.Models;
using Ipe.UseCases.Interfaces.Repositories;

namespace Ipe.UseCases.Interfaces
{
	public interface IMailVerificationRepository : IBaseRepository<MailVerification>
	{
		MailVerification GetLastRegisterByEmail(string Email);
	}
}

