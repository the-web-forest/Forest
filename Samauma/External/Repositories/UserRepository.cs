using Samauma.Domain.Models;
using Samauma.UseCases.Interfaces;
using MongoDB.Driver;
namespace Samauma.External.Repositories
{
	public class UserRepository : BaseRepository<User>, IUserRepository
	{
        public UserRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }

        public async Task<long> CountUsers()
        {
            var Query = _collection.Find(x => x.Name != null);
            var TotalTask = await Query.CountDocumentsAsync();
            return TotalTask;
        }

        public async Task<User> GetByEmail(string Email)
        {
            return await _collection.Find(x => x.Email == Email).FirstOrDefaultAsync();
        }

        public async Task<User> GetById(string UserId)
        {
            return await _collection.Find(x => x.Id == UserId).FirstOrDefaultAsync();
        }

        public async Task<List<User>> ListUsersPerPage(int Page, int ItensPerPage)
        {
            var SkipQuantity = (Page == 1) ? 0 : ((Page - 1) * ItensPerPage);
            var Query = _collection.Find(x => x.Name != null);
            var Results = await Query.Skip(SkipQuantity).Limit(ItensPerPage).ToListAsync();
            return Results;
           
        }
    }
}

