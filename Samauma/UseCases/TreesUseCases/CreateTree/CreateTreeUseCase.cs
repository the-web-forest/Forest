using Samauma.Domain.Models;
using Samauma.UseCases.Interfaces;
using Samauma.Domain.Errors;

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
            if (await VerifyTreeNameExists(Input.Name))
                throw new InvalidTreeNameException();

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

        private async Task<bool> VerifyTreeNameExists(string Name)
        {
            var treeFound = await _treeRepository.GetTreeByName(Name);
            if(treeFound != null)
                return true;

            return false;
        }
    }
}
