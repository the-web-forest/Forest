using Ipe.Domain.Models;
using Ipe.UseCases.Interfaces.Repositories;
using MongoDB.Driver;

namespace Ipe.External.Repositories
{
    public class TreeRepository : BaseRepository<Tree>, ITreeRepository
    {
        public TreeRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }

        public async Task<List<Tree>> GetTreesById(List<string> TreeId)
        {
            return await _collection.Find(x => TreeId.Contains(x.Id)).ToListAsync();
        }
    }
}
