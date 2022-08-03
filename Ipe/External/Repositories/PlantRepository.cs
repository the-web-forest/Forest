using Ipe.Domain.Models;
using Ipe.UseCases.Interfaces.Repositories;
using MongoDB.Driver;

namespace Ipe.External.Repositories
{
    public class PlantRepository : BaseRepository<Plant>, IPlantRepository
    {
        public PlantRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }

        public async Task<Plant> FindSomePlantByUserId(string UserId)
        {
            return await _collection.Find(x => x.UserId == UserId).FirstOrDefaultAsync();
        }
    }
}
