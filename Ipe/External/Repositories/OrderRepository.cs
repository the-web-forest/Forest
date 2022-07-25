using Ipe.Domain.Models;
using Ipe.UseCases.Interfaces.Repositories;
using MongoDB.Driver;

namespace Ipe.External.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }
    }
}
