using Samauma.Domain.Models;
using Samauma.UseCases.Interfaces;

namespace Samauma.UseCases.CreateTree
{
    public class CreateTreeUseCase : IUseCase<CreateTreeUseCaseInput, CreateTreeUseCaseOutput>
    {
        private readonly ITreeRepository _treeRepository;

        public CreateTreeUseCase(
            ITreeRepository treeRepository
            )
        {
            _treeRepository = treeRepository;
        }

        public async Task<CreateTreeUseCaseOutput> Run(CreateTreeUseCaseInput Input)
        {
            await CreateTree(Input);
            return new CreateTreeUseCaseOutput();
        }

        private async Task CreateTree(CreateTreeUseCaseInput input)
        {
            await _treeRepository.Create(new Tree
            {
                Name = input.Name,
                Description = input.Description,
                Value = input.Value,
                Biome = input.Biome,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });
        }
    }
}
