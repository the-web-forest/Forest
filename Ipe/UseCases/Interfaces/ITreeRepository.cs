using Ipe.UseCases.Interfaces.Repositories;
using Ipe.Domain.Models;

namespace Ipe.UseCases.Interfaces
{
    public interface ITreeRepository : IBaseRepository<Tree>
    {
        Task<Tree> GetById(string TreeId);
    }
}
