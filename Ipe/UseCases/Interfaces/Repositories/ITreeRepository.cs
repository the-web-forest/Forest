using Ipe.Domain.Models;

namespace Ipe.UseCases.Interfaces.Repositories
{
    public interface ITreeRepository : IBaseRepository<Tree>
    {
        Task<List<Tree>> GetTreesById(List<string> TreeId);
    }
}
