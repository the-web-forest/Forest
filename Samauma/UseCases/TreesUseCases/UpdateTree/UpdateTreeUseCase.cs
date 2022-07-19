using Samauma.Domain.Models;
using Samauma.UseCases.Interfaces;
using Samauma.Domain.Errors;
using Samauma.UseCases.Interfaces.Services;

namespace Samauma.UseCases
{
    public class UpdateTreeUseCase : IUseCase<UpdateTreeUseCaseInput, UpdateTreeUseCaseOutput>
    {
        private readonly ITreeRepository _treeRepository;
        private readonly IStorageService _storageService;

        public UpdateTreeUseCase(
            ITreeRepository treeRepository,
            IStorageService storageService
            )
        {
            _treeRepository = treeRepository;
            _storageService = storageService;
        }

        public async Task<UpdateTreeUseCaseOutput> Run(UpdateTreeUseCaseInput Input)
        {
            var TreeToUpdate = await _treeRepository.GetTreeById(Input.Id);

            if(TreeToUpdate is null)
            {
                throw new InvalidTreeIdException();
            }

            await ValidateTreeUpdate(TreeToUpdate, Input);
            await UpdateTree(TreeToUpdate, Input);

            return new UpdateTreeUseCaseOutput();
        }

        private async Task UpdateTree(Tree Tree, UpdateTreeUseCaseInput Input)
        {
            var HasNewImage = Input.ImageBase64 is not null && Input.ImageBase64 != "";
            var ImageUri = Tree.Image;

            if (HasNewImage)
            {
                ImageUri = UploadNewTreeImage(Input.ImageBase64);
            }


            Tree.Name = Input.Name;
            Tree.Description = Input.Description;
            Tree.Value = Input.Value;
            Tree.Biome = Input.Biome;
            Tree.UpdatedAt = DateTime.Now;
            Tree.Image = ImageUri;
            await _treeRepository.Update(Tree);
        }

        private string UploadNewTreeImage(string Base64Image)
        {
            return _storageService.UploadTreeImageInBase64(Base64Image);
        }

        private async Task<bool> ValidateTreeUpdate(Tree Tree, UpdateTreeUseCaseInput NewTree)
        {
            var TreeWithSameName = await _treeRepository.GetTreeByName(NewTree.Name);

            if(TreeWithSameName is not null &&
                TreeWithSameName.Id != Tree.Id &&
                TreeWithSameName.Deleted == false
            )
            {
                throw new InvalidTreeNameException();
            }

            return true;    
        }
    }
}
