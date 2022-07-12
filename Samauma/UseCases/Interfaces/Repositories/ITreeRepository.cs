using Samauma.UseCases.Interfaces.Repositories;
using Samauma.Domain.Models;

namespace Samauma.UseCases.Interfaces
{
    public interface ITreeRepository : IBaseRepository<Tree>
    {
        Task<Tree> GetById(string TreeId);
        Task<List<Tree>> ListTreesPerPage(int Page, int ItensPerPage);
        Task<long> CountTrees();
    }
}
