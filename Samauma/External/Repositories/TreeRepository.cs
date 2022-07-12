using Samauma.Domain.Models;
using Samauma.UseCases.Interfaces;
using MongoDB.Driver;

namespace Samauma.External.Repositories
{
    public class TreeRepository : BaseRepository<Tree>, ITreeRepository
    {
        public TreeRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }

        public async Task<long> CountTrees()
        {
            var Query = _collection.Find(x => x.Name != null);
            var TotalTask = await Query.CountDocumentsAsync();
            return TotalTask;
        }

        public Task<Tree> GetById(string TreeId)
        {
            return null;
        }

        public async Task<List<Tree>> ListTreesPerPage(int Page, int ItensPerPage)
        {
            var SkipQuantity = (Page == 1) 
                ? 0 
                : ((Page - 1) * ItensPerPage);

            var Query = _collection
                .Find(x => x.Name != null);

            var Results = await Query
                .Skip(SkipQuantity)
                .Limit(ItensPerPage)
                .ToListAsync();
                
            return Results;
        }
    }
}
