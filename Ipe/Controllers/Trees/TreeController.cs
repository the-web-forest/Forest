using FluentResults;
using Ipe.Controllers.Trees.DTOs;
using Ipe.UseCases;
using Ipe.UseCases.TreeUseCase.GetTreesByFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ipe.Controllers.Trees;

[ApiController]
[Route("[controller]")]
[Authorize]
public class TreeController : Controller
{
    private readonly ILogger<TreeController> logger;
    private readonly IUseCase<GetTreesByFilterInputUseCase, Result<GetTreesByFilterOutputUseCase>> getTreesByFilterUseCase;

    public TreeController(
        ILogger<TreeController> logger,
        IUseCase<GetTreesByFilterInputUseCase, Result<GetTreesByFilterOutputUseCase>> getTreesByFilterUseCase)
    {
        this.logger = logger;
        this.getTreesByFilterUseCase = getTreesByFilterUseCase;
    }

    [HttpGet]
    public async Task<ObjectResult> GetTreesByBiome([FromQuery] TreeSearch filter)
    {
        logger.LogInformation("Gettig trees by filter");
        var trees = await getTreesByFilterUseCase
            .Run(new GetTreesByFilterInputUseCase {
                Biome = filter.Biome,
                RequiredTotal = filter.RequiredTotal,
                Skip = filter.Skip,
                Take = filter.Take
            });

        ObjectResult result;

        if (!trees.IsSuccess)
        {
            switch (trees.Errors.First().Message)
            {
                case "NotFound":
                    {
                        result = NotFound(trees.Reasons.Last());
                        break;
                    }
                case "Internal error":
                    {
                        result = StatusCode(StatusCodes.Status500InternalServerError, trees.Reasons.Last());
                        break;
                    }
                default:
                    {
                        result = BadRequest(trees.Reasons.Last());
                        break;
                    }
            }
        }
        else
        {
            result = Ok(trees.Value);
        }

        return result;
    }
}
