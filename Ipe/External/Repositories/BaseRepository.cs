using MongoDB.Driver;
using Ipe.Domain.Models;

namespace Ipe.External.Repositories
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
			Data.CreatedAt = DateTime.Now;
			Data.UpdatedAt = DateTime.Now;
            await _collection.InsertOneAsync(Data);
        }

		public async Task CreateMany(List<T> Data)
		{
			for(int i = 0; i< Data.Count; i++)
            {
				Data[i].CreatedAt = DateTime.Now;
				Data[i].UpdatedAt = DateTime.Now;
            }
			await _collection.InsertManyAsync(Data);
		}

		public async Task Update(T Data)
        {
			Data.UpdatedAt = DateTime.Now;
			await _collection.ReplaceOneAsync(doc => doc.Id == Data.Id, Data);
        }
	}
}

