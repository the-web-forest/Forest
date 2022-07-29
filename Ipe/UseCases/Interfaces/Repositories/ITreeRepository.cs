using Ipe.Domain.Models;
using Ipe.UseCases.TreeUseCase.GetTreesByFilter;

namespace Ipe.UseCases.Interfaces.Repositories
{
    public interface ITreeRepository : IBaseRepository<Tree>
    {
        Task<List<Tree>> GetTreesById(List<string> TreeId);
        Task<GetTreesByFilterOutputUseCase> GetTreesByFilter(GetTreesByFilterInputUseCase filter);
    }
}
