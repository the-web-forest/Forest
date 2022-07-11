using Ipe.Domain.Models;
using Ipe.UseCases.Interfaces;
using MongoDB.Driver;

namespace Ipe.External.Repositories
{
    public class TreeRepository : BaseRepository<Tree>, ITreeRepository
    {
        public TreeRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }

        public Task<Tree> GetById(string TreeId)
        {
            return null;
        }
    }
}
