using Samauma.Domain.Models;
using Samauma.UseCases.Interfaces;
using Samauma.Domain.Errors;
using Samauma.UseCases.Interfaces.Services;

namespace Samauma.UseCases.CreateTree
{
    public class CreateTreeUseCase : IUseCase<CreateTreeUseCaseInput, CreateTreeUseCaseOutput>
    {
        private readonly ITreeRepository _treeRepository;
        private readonly IStorageService _storageService;

        public CreateTreeUseCase(
            ITreeRepository treeRepository,
            IStorageService storageService
            )
        {
            _treeRepository = treeRepository;
            _storageService = storageService;
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

            var ImageUri = _storageService.UploadTreeImageInBase64(input.Base64Image);

            await _treeRepository.Create(new Tree
            {
                Name = input.Name,
                Description = input.Description,
                Image = ImageUri,
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
