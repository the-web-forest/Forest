using Ipe.Domain.Models;
using Ipe.UseCases.Interfaces;
using MongoDB.Driver;
namespace Ipe.External.Repositories
{
    public class StateRepository : BaseRepository<State>, IStateRepository
    {
        public StateRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }

        public List<State> FindAll()
        {
            return _collection.Find(x => true).ToList();
        }

        public State FindStateByInitial(string StateInitial)
        {
            return _collection.Find(x => x.Initials == StateInitial).FirstOrDefault();
        }

    }
}

