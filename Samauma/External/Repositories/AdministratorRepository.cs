using MongoDB.Driver;
using Samauma.Domain.Models;
using Samauma.UseCases.Interfaces;

namespace Samauma.External.Repositories
{
	public class AdministratorRepository: BaseRepository<Administrator>, IAdministratorRepository
    {
        public AdministratorRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }

        public async Task<Administrator> GetByEmail(string Email)
        {
            return await _collection.Find(x => x.Email == Email).FirstOrDefaultAsync();
        }

        public async Task<Administrator> GetById(string UserId)
        {
            return await _collection.Find(x => x.Id == UserId).FirstOrDefaultAsync();
        }
    }
}

