using Ipe.Domain.Models;
using Ipe.UseCases.Interfaces;
using MongoDB.Driver;

namespace Ipe.External.Repositories
{
    public class PasswordResetRepository : BaseRepository<PasswordReset>, IPasswordResetRepository
    {
        public PasswordResetRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }

        PasswordReset IPasswordResetRepository.GetLastRegisterByEmail(string Email)
        {
            return _collection
                .Find(x => x.Email == Email)
                .SortByDescending(x => x.CreatedAt)
                .FirstOrDefault();
        }
    }
}

