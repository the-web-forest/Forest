using FluentResults;
using Ipe.UseCases.Interfaces.Repositories;

namespace Ipe.UseCases.TreeUseCase.GetTreesByFilter
{
    public class GetTreesByFilterUseCase : IUseCase<GetTreesByFilterInputUseCase, Result<GetTreesByFilterOutputUseCase>>
    {
        private readonly ITreeRepository treesRepository;

        public GetTreesByFilterUseCase(ITreeRepository treesRepository)
        {
            this.treesRepository = treesRepository;
        }

        public async Task<Result<GetTreesByFilterOutputUseCase>> Run(GetTreesByFilterInputUseCase input)
        {
            var findResult = new Result<GetTreesByFilterOutputUseCase>();

            try
            {
                var foundTrees = await treesRepository.GetTreesByFilter(input);

                if (foundTrees.Trees.Count == 0)
                    findResult.WithError("No trees found by the filter");
                else
                    findResult.WithValue(foundTrees);
            }
            catch (Exception ex)
            {
                findResult.WithError($"Error! An error ocurred trying to find trees by filter.\n{ex.InnerException}");
            }

            return findResult;
        }
    }
}
