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
            if (!(await CheckIdExistence(Input.Id)))
                throw new InvalidTreeIdException();

            await UpdateTree(Input);
            return new UpdateTreeUseCaseOutput();
        }

        private async Task UpdateTree(UpdateTreeUseCaseInput input)
        {
            await _treeRepository.Update(new Tree
            {
                Id = input.Id,
                Name = input.Name,
                Description = input.Description,
                Value = input.Value,
                Biome = input.Biome,
                UpdatedAt = DateTime.Now
            });
        }

        private async Task<bool> CheckIdExistence(string Id)
        {
            var TreeFound = await _treeRepository.GetTreeById(Id);

            if (TreeFound == null)
                return false;

            return true;    
        }
    }
}
