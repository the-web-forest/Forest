using Microsoft.AspNetCore.Mvc;
using Ipe.UseCases.CreateTree;
using Ipe.UseCases.ListTrees;
using Ipe.UseCases;
using Ipe.Domain.Errors;
using Ipe.Controllers.Trees.DTOS;

namespace Ipe.Controllers.Trees
{
    [ApiController]
    [Route("Trees")]
    public class TreesController : ControllerBase
    {

        private readonly IUseCase<CreateTreeUseCaseInput, CreateTreeUseCaseOutput> _createTreeUseCase;
        private readonly IUseCase<ListTreesUseCaseInput, ListTreesUseCaseOutput> _listTreesUseCase;

        public TreesController(
            IUseCase<CreateTreeUseCaseInput, CreateTreeUseCaseOutput> createTreeUseCase,
            IUseCase<ListTreesUseCaseInput, ListTreesUseCaseOutput> listTreesUseCase
            )
        {
            _createTreeUseCase = createTreeUseCase;
            _listTreesUseCase = listTreesUseCase;
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

        [HttpGet("")]
        public async Task<ObjectResult> GetTree([FromQuery] int Page)
        {
            Page = (Page < 1) ? 1 : Page;

            try
            {
                var Data = await _listTreesUseCase.Run(new ListTreesUseCaseInput
                {
                    Page = Page
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
