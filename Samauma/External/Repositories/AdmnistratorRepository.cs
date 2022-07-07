using System;
using MongoDB.Driver;
using Samauma.Domain.Models;
using Samauma.UseCases.Interfaces;

namespace Samauma.External.Repositories
{
	public class AdmnistratorRepository: BaseRepository<Admnistrator>, IAdmnistratorRepository
    {
        public AdmnistratorRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }

        public async Task<Admnistrator> GetByEmail(string Email)
        {
            return await _collection.Find(x => x.Email == Email).FirstOrDefaultAsync();
        }

        public async Task<Admnistrator> GetById(string UserId)
        {
            return await _collection.Find(x => x.Id == UserId).FirstOrDefaultAsync();
        }
    }
}

