using Samauma.Domain.Models;
using Samauma.UseCases.Interfaces;
using Samauma.Domain.Errors;

namespace Samauma.UseCases
{
    public class UpdateTreeUseCase : IUseCase<UpdateTreeUseCaseInput, UpdateTreeUseCaseOutput>
    {
        private readonly ITreeRepository _treeRepository;

        public UpdateTreeUseCase(
            ITreeRepository treeRepository
            )
        {
            _treeRepository = treeRepository;
        }

        public async Task<UpdateTreeUseCaseOutput> Run(UpdateTreeUseCaseInput Input)
        {
            var TreeToUpdate = await _treeRepository.GetTreeById(Input.Id);
            await ValidateTreeUpdate(TreeToUpdate, Input);
            await UpdateTree(TreeToUpdate, Input);
            return new UpdateTreeUseCaseOutput();
        }

        private async Task UpdateTree(Tree Tree, UpdateTreeUseCaseInput Input)
        {
            Tree.Name = Input.Name;
            Tree.Description = Input.Description;
            Tree.Value = Input.Value;
            Tree.Biome = Input.Biome;
            Tree.UpdatedAt = DateTime.Now;
            await _treeRepository.Update(Tree);
        }

        private async Task<bool> ValidateTreeUpdate(Tree Tree, UpdateTreeUseCaseInput NewTree)
        {
            var TreeWithSameName = await _treeRepository.GetTreeByName(NewTree.Name);

            if(TreeWithSameName.Id != Tree.Id && TreeWithSameName.Deleted == false)
            {
                throw new InvalidTreeNameException();
            }

            return true;    
        }
    }
}
