using Ipe.Domain.Models;

namespace Ipe.UseCases.Interfaces.Repositories
{
    public interface IPlantRepository : IBaseRepository<Plant>
    {
        Task CreateMany(IEnumerable<Plant> plants);
    }
}