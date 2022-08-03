using Ipe.Domain.Models;
using Ipe.UseCases.Interfaces.Repositories;
using Ipe.UseCases.TreeUseCase.GetTreesByFilter;
using MongoDB.Bson;
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

        public async Task<GetTreesByFilterOutputUseCase> GetTreesByFilter(GetTreesByFilterInputUseCase filter)
        {
            var builder = Builders<Tree>.Filter;

            var mongoFilter = builder.Empty;
            mongoFilter &= builder.Eq("deleted", false);

            if (!String.IsNullOrEmpty(filter.Biome))
                mongoFilter &= builder.Regex("Biome", new BsonRegularExpression(filter.Biome));

            var allTrees = _collection
                .Find(mongoFilter);

            long? total = null;
            if (filter.RequiredTotal is not null && filter.RequiredTotal == true)
                total = allTrees.CountDocuments();

            /*allTrees
                .Sort(Builders<Tree>.Sort.Ascending("name"));*/

            if (filter.Take is not null)
                allTrees
                    .Limit(filter.Take);

            if (filter.Skip is not null)
                allTrees
                    .Skip(filter.Skip ?? 0);

            return await allTrees
                .ToListAsync()
                .ContinueWith(trees => new GetTreesByFilterOutputUseCase
                {
                    Trees = trees.Result.Select(tree => new TreeByFilter(
                        tree.Name,
                        tree.Description,
                        tree.Image,
                        tree.Value,
                        tree.Biome
                    )),
                    TotalCount = total
                });
        }
    }
}
