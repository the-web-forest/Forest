using Samauma.UseCases.Interfaces;
using Samauma.Domain.Errors;

namespace Samauma.UseCases.GetTreeById
{
    public class GetTreeByIdUseCase : IUseCase<GetTreeByIdUseCaseInput, GetTreeByIdUseCaseOutput>
    {
        private readonly ITreeRepository _treeRepository;

        public GetTreeByIdUseCase (ITreeRepository treeRepository)
        {
            _treeRepository = treeRepository;
        }

        public async Task<GetTreeByIdUseCaseOutput> Run(GetTreeByIdUseCaseInput input)
        {
            var Tree = await _treeRepository.GetTreeById(input.Id);
            if(Tree == null)
                throw new InvalidTreeIdException();

            return new GetTreeByIdUseCaseOutput
            {
                Tree = Tree,
            };
        } 
    }
}
