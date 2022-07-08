using Samauma.Domain.Models;
using Samauma.UseCases.Interfaces.Repositories;

namespace Samauma.UseCases.Interfaces
{
    public interface IAdministratorRepository : IBaseRepository<Administrator>
    {
        Task<Administrator> GetByEmail(string Email);
        Task<Administrator> GetById(string UserId);
    }
}

