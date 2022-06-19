using Ipe.Domain.Models;
using Ipe.UseCases.Interfaces;
using MongoDB.Driver;
namespace Ipe.External.Repositories
{
	public class UserRepository : BaseRepository<User>, IUserRepository
	{
        public UserRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }

        public async Task<User> GetByEmail(string Email)
        {
            return await _collection.Find(x => x.Email == Email).FirstOrDefaultAsync();
        }
	}
}

