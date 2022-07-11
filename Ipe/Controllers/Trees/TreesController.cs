using Microsoft.AspNetCore.Mvc;
using Ipe.UseCases.CreateTree;
using Ipe.UseCases;
using Ipe.Domain.Errors;
using Ipe.Controllers.Trees.DTOS;

namespace Ipe.Controllers.Trees
{
    [ApiController]
    [Route("[controller]")]
    public class TreesController : ControllerBase
    {

        private readonly IUseCase<CreateTreeUseCaseInput, CreateTreeUseCaseOutput> _createTreeUseCase;

        public TreesController(
            IUseCase<CreateTreeUseCaseInput, CreateTreeUseCaseOutput> createTreeUseCase
            )
        {
            _createTreeUseCase = createTreeUseCase;
        }

        [HttpPost]
        public async Task<ObjectResult> CreateTree([FromBody]CreateTreeInput Input)
        {
            //Personal doubt: The authentication from the user should be done in a attribute?
            try
            {
                var Data = await _createTreeUseCase.Run(new CreateTreeUseCaseInput
                {
                    Name = Input.Name,
                    Description = Input.Description,
                    Biome = Input.Biome,
                    Value = Input.Value
                });
                return new ObjectResult(Data);
            }
            catch (BaseException e)
            {
                return new BadRequestObjectResult(e.Data);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
