using Samauma.Domain.Errors;
using Samauma.Domain.Models;
using Samauma.UseCases.Interfaces;

namespace Samauma.UseCases.ListTrees
{
    public class ListTreesUseCase : IUseCase<ListTreesUseCaseInput, ListTreesUseCaseOutput>
    {
        private readonly ITreeRepository _treeRepository;
        private readonly int ITENS_PER_REQUEST = 50;

        public ListTreesUseCase(ITreeRepository treeRepository)
        {
            _treeRepository = treeRepository;
        }

        public async Task<ListTreesUseCaseOutput> Run(ListTreesUseCaseInput Input)
        {
            var TotalTrees = await GetTotalTrees();
            var TotalPages = GetTotalPages(TotalTrees);

            if(Input.Page > TotalPages)
            {
                throw new OutOfRangeException();
            }

            var Trees = await GetTrees(Input.Page);
            var Response = BuildResponse(Input.Page, TotalPages, Trees);
            return Response;
        }

        private async Task<long> GetTotalTrees()
            => await _treeRepository.CountTrees();

        private long GetTotalPages(long TotalTrees)
        {
            if(TotalTrees < ITENS_PER_REQUEST)
            {
                return 1;
            }

            double Pages = TotalTrees / (double)ITENS_PER_REQUEST;
           
            return (long)Math.Ceiling(Pages);
        }

        private async Task<List<Tree>> GetTrees(int Page)
            => await _treeRepository.ListTreesPerPage(Page, ITENS_PER_REQUEST);

        private ListTreesUseCaseOutput BuildResponse(int CurrentPage, long TotalPages, List<Tree> Trees)
        {
            var ListOfTrees = Trees.Select(x => DTOS.LightTree.FromTree(x)).ToList();
            
            return new ListTreesUseCaseOutput
            {
                TotalPages = (int)TotalPages,
                CurrentPage = CurrentPage,
                ItemsPerPage = ITENS_PER_REQUEST,
                Trees = ListOfTrees
            };
        }
    }
}