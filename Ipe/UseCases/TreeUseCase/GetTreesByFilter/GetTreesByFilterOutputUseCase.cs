using Ipe.Domain.Models;

namespace Ipe.UseCases.TreeUseCase.GetTreesByFilter
{
    public class GetTreesByFilterOutputUseCase
    {
        public IEnumerable<TreeByFilter> Trees { get; set; }
        public long? TotalCount { get; set; }
    }
}
