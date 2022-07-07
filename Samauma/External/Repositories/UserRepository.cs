using Samauma.Domain.Models;
using Samauma.UseCases.Interfaces;
using MongoDB.Driver;
namespace Samauma.External.Repositories
{
	public class UserRepository : BaseRepository<User>, IUserRepository
	{
        public UserRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }

        public async Task<User> GetByEmail(string Email)
        {
            return await _collection.Find(x => x.Email == Email).FirstOrDefaultAsync();
        }

        public async Task<User> GetById(string UserId)
        {
            return await _collection.Find(x => x.Id == UserId).FirstOrDefaultAsync();
        }
    }
}

