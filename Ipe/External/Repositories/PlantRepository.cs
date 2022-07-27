using Ipe.Domain.Models;
using Ipe.UseCases.Interfaces.Repositories;
using MongoDB.Driver;

namespace Ipe.External.Repositories
{
    public class PlantRepository : BaseRepository<Plant>, IPlantRepository
    {
        public PlantRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }
    }
}
