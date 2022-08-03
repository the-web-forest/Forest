using Ipe.Domain.Models;

namespace Ipe.UseCases.Interfaces.Repositories
{
    public interface IPlantRepository : IBaseRepository<Plant>
    {
        Task<Plant> FindSomePlantByUserId(string UserId);
    }
}