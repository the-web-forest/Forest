using Samauma.Domain.Models;
using Samauma.UseCases.Interfaces.Repositories;

namespace Samauma.UseCases.Interfaces
{
    public interface IAdmnistratorRepository : IBaseRepository<Admnistrator>
    {
        Task<Admnistrator> GetByEmail(string Email);
        Task<Admnistrator> GetById(string UserId);
    }
}

