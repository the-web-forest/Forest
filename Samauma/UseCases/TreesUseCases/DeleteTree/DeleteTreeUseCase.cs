using Samauma.Domain.Errors;
using Samauma.Domain.Models;
using Samauma.UseCases.Interfaces;

namespace Samauma.UseCases.DeleteTree;

public class DeleteTreeUseCase : IUseCase<DeleteTreeUseCaseInput, DeleteTreeUseCaseOutput>
{
    private readonly ITreeRepository _treeRepository;

    public DeleteTreeUseCase(ITreeRepository treeRepository)
    {
        _treeRepository = treeRepository;
    }

    public async Task<DeleteTreeUseCaseOutput> Run(DeleteTreeUseCaseInput Input)
    {
        var TreeToDelete = await _treeRepository.GetTreeById(Input.Id);

        if (TreeToDelete is null)
            throw new InvalidTreeIdException();

        if(TreeIsActive(TreeToDelete))
            await DeleteTree(TreeToDelete);

        return new DeleteTreeUseCaseOutput();
    }

    private bool TreeIsActive(Tree TreeToDelete)
        => TreeToDelete.Deleted == false;

    private async Task DeleteTree(Tree TreeToDelete)
    {
        TreeToDelete.UpdatedAt = DateTime.Now;
        TreeToDelete.Deleted = true;

        await _treeRepository.Update(TreeToDelete);
    }
}
