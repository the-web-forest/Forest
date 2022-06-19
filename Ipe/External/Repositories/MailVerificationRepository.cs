using Ipe.Domain.Models;
using Ipe.UseCases.Interfaces;
using MongoDB.Driver;

namespace Ipe.External.Repositories
{
	public class MailVerificationRepository : BaseRepository<MailVerification>, IMailVerificationRepository
	{
        public MailVerificationRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) {}

        public MailVerification GetLastRegisterByEmail(string Email)
        {
            return _collection
                .Find(x => x.Email == Email)
                .SortByDescending(x => x.CreatedAt)
                .FirstOrDefault();
        }
    }
}

