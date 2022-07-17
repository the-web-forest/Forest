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

        public async Task<long> CountActiveTrees()
        {
            var Query = _collection.Find(x => x.Name != null && x.Deleted == false);
            var TotalTask = await Query.CountDocumentsAsync();
            return TotalTask;
        }

        public async Task<Tree> GetTreeById(string TreeId)
        {
            return await _collection.Find(x => x.Id == TreeId).FirstOrDefaultAsync();
        }

        public async Task<Tree> GetTreeByName(string Name)
        {
            return await _collection.Find(x => x.Name == Name).FirstOrDefaultAsync();
        }

        public async Task<Tree> GetActiveTreeByName(string Name)
        {
            return await _collection.Find(x => x.Name == Name && x.Deleted == false).FirstOrDefaultAsync();
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

        public async Task<List<Tree>> ListActiveTreesPerPage(int Page, int ItensPerPage)
        {
            var SkipQuantity = (Page == 1) ? 0 : ((Page - 1) * ItensPerPage);

            var Query = _collection
                .Find(x => x.Name != null && x.Deleted == false);

            var Results = await Query
                .Skip(SkipQuantity)
                .Limit(ItensPerPage)
                .ToListAsync();

            return Results;
        }

    }
}
