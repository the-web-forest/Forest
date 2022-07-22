using Ipe.Controllers.Plant.DTOs;
using Ipe.Domain.Errors;
using Ipe.UseCases;
using Ipe.UseCases.GetCitiesByState;
using Ipe.UseCases.GetStates;
using Microsoft.AspNetCore.Mvc;

namespace Ipe.Controllers.Cities;

[ApiController]
[Route("[controller]")]
public class PlantController : Controller
{

    public PlantController()
    {
       
    }

    [HttpPost]
    public ObjectResult Plant([FromBody] PlantInput Input)
    {
        try
        {
            return new ObjectResult(Input);
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

