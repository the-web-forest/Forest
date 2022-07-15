using Microsoft.AspNetCore.Mvc;
using Samauma.UseCases.CreateTree;
using Samauma.UseCases.ListTrees;
using Samauma.UseCases.GetTreeById;
using Samauma.UseCases;
using Samauma.Domain.Errors;
using Samauma.Controllers.Trees.DTOS;
using Microsoft.AspNetCore.Authorization;

namespace Samauma.Controllers.Trees
{
    [ApiController]
    [Route("Trees")]
    public class TreesController : ControllerBase
    {
        private readonly IUseCase<CreateTreeUseCaseInput, CreateTreeUseCaseOutput> _createTreeUseCase;
        private readonly IUseCase<UpdateTreeUseCaseInput, UpdateTreeUseCaseOutput> _updateTreeUseCase;
        private readonly IUseCase<ListTreesUseCaseInput, ListTreesUseCaseOutput> _listTreesUseCase;
        private readonly IUseCase<GetTreeByIdUseCaseInput, GetTreeByIdUseCaseOutput> _getTreeById;   

        public TreesController(
            IUseCase<CreateTreeUseCaseInput, CreateTreeUseCaseOutput> createTreeUseCase,
            IUseCase<UpdateTreeUseCaseInput, UpdateTreeUseCaseOutput> updateTreeUseCase,
            IUseCase<ListTreesUseCaseInput, ListTreesUseCaseOutput> listTreesUseCase,
            IUseCase<GetTreeByIdUseCaseInput, GetTreeByIdUseCaseOutput> getTreeById
            )
        {
            _createTreeUseCase = createTreeUseCase;
            _updateTreeUseCase = updateTreeUseCase;
            _listTreesUseCase = listTreesUseCase;
            _getTreeById = getTreeById;
        }

        [HttpPost]
        [Authorize]
        public async Task<ObjectResult> CreateTree([FromBody]CreateTreeInput Input)
        {
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

        [HttpPut]
        [Authorize]
        public async Task<ObjectResult> UpdateTree([FromBody]UpdateTreeInput Input)
        {
            try
            {
                var Data = await _updateTreeUseCase.Run(new UpdateTreeUseCaseInput
                {
                    Id = Input.Id,
                    Name = Input.Name,
                    Description= Input.Description,
                    Biome= Input.Biome,
                    Value= Input.Value
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

        [HttpGet("List")]
        [Authorize]
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

        [HttpGet("{Id}")]
        [Authorize]
        public async Task<ObjectResult> GetTreeById(string Id)
        {
            try
            {
                var Data = await _getTreeById.Run(new GetTreeByIdUseCaseInput
                {
                    Id = Id
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
