using Ipe.Controllers.Plant.DTOs;
using Ipe.Domain.Errors;
using Ipe.UseCases;
using Ipe.UseCases.PlantUseCase.CreatePlant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ipe.Controllers.Cities;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PlantController : Controller
{
    private readonly ILogger<PlantController> _logger;
    private readonly IUseCase<PlantUseCaseInput, PlantUseCaseOutput> _createPlantUseCase;

    public PlantController(ILogger<PlantController> logger,
        IUseCase<PlantUseCaseInput, PlantUseCaseOutput> createPlantUseCase)
    {
        _logger = logger;
        _createPlantUseCase = createPlantUseCase;
    }

    [HttpPost]
    public async Task<ObjectResult> Plant([FromBody] PlantInput Input)
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        _logger.LogInformation("Create plant order for User => {UserId}", userId);

        try
        {
            var Data = await _createPlantUseCase.Run(new PlantUseCaseInput
            {
                UserId = userId,
                CardToken = Input.CardToken,
                Trees = Input.Trees
                    .Select(tree => new TreeUseCaseInput
                    {
                        Id = tree.Id,
                        Hastags = tree.Hastags,
                        Message = tree.Message,
                        Name = tree.Name
                    })
                    .ToList()
            });

            return new ObjectResult(Data);
        }
        catch (BaseException e)
        {
            return new BadRequestObjectResult(e.Data);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}

