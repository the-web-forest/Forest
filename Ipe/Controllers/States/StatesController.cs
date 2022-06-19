using Ipe.Domain.Errors;
using Ipe.UseCases;
using Ipe.UseCases.GetCitiesByState;
using Ipe.UseCases.GetStates;
using Microsoft.AspNetCore.Mvc;

namespace Ipe.Controllers.Cities;

[ApiController]
[Route("[controller]")]
public class StatesController : Controller
{

    private readonly IUseCase<GetStatesUseCaseInput, GetStatesUseCaseOutput> _getStatesUseCase;
    private readonly IUseCase<GetCitiesByStateUseCaseInput, GetCitiesByStateUseCaseOutput> _getCitiesByStateUseCase;

    public StatesController(
        IUseCase<GetStatesUseCaseInput, GetStatesUseCaseOutput> getStatesUseCase,
        IUseCase<GetCitiesByStateUseCaseInput, GetCitiesByStateUseCaseOutput> getCitiesByStateUseCase
        )
    {
        _getStatesUseCase = getStatesUseCase;
        _getCitiesByStateUseCase = getCitiesByStateUseCase;
    }

    [HttpGet]
    public async Task<ObjectResult> GetStates()
    {
        GetStatesUseCaseOutput Data = await _getStatesUseCase.Run(new GetStatesUseCaseInput());
        return new ObjectResult(Data);
    }

    [HttpGet]
    [Route("{State}/Cities")]
    public async Task<ObjectResult> GetCitiesByState(
         string State
        )
    {
        try
        {
            GetCitiesByStateUseCaseOutput Data = await _getCitiesByStateUseCase.Run(new GetCitiesByStateUseCaseInput
            {
                State = State
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

