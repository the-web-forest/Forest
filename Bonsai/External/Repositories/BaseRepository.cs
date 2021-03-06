using MongoDB.Driver;
using Bonsai.Domain.Models;
using MongoDB.Bson;

namespace Bonsai.External.Repositories
{
	public abstract class BaseRepository<T> where T: Model
	{

        private readonly IMongoDatabase _mongoDatabase;
		protected readonly IMongoCollection<T> _collection;

		public BaseRepository(IMongoDatabase mongoDatabase)
		{
			_mongoDatabase = mongoDatabase;
			_collection = _mongoDatabase.GetCollection<T>(typeof(T).Name);
		}

		public async Task Create(T Data)
        {
			await _collection.InsertOneAsync(Data);
        }

		public async Task Update(T Data)
        {
			await _collection.ReplaceOneAsync(doc => doc.Id == Data.Id, Data);
        }

	}
}

