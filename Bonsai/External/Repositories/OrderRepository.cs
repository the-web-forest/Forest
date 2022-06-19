using Bonsai.Domain.Models;
using Bonsai.UseCases.Interfaces;
using MongoDB.Driver;

namespace Bonsai.External.Repositories
{
	public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) {}
    }
}

