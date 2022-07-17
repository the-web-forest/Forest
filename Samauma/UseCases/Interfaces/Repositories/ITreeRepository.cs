using Samauma.UseCases.Interfaces.Repositories;
using Samauma.Domain.Models;

namespace Samauma.UseCases.Interfaces
{
    public interface ITreeRepository : IBaseRepository<Tree>
    {
        Task<Tree> GetTreeById(string TreeId);
        Task<Tree> GetTreeByName(string TreeId);
        Task<List<Tree>> ListTreesPerPage(int Page, int ItensPerPage);
        Task<List<Tree>> ListActiveTreesPerPage(int Page, int ItensPerPage);
        Task<long> CountTrees();
        Task<Tree> GetActiveTreeByName(string Name);
    }
}
