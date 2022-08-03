using FluentResults;
using Ipe.UseCases.Interfaces.Repositories;
using Ipe.Util;

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

                if (!foundTrees.Trees.Any())
                {
                    findResult.WithError("Not Found").WithReason(new ReasonBuilder("No trees has found"));
                }
                else
                {
                    findResult.WithValue(foundTrees);
                }
            }
            catch (Exception ex)
            {
                findResult.WithError("Internal error").WithReason(new ReasonBuilder(ex.InnerException.Message));
            }

            return findResult;
        }
    }
}
