using Samauma.Domain.Errors;
using Samauma.UseCases;
using Microsoft.AspNetCore.Mvc;
using Samauma.UseCases.AdministratorLogin;

namespace Samauma.Controllers.Administrator;

[ApiController]
[Route("[controller]")]
public class AdministratorController : Controller
{

    private readonly IUseCase<AdministratorLoginUseCaseInput, AdministratorLoginUseCaseOutput> _admnistratorLoginUseCase;
    private readonly ILogger<AdministratorController> _logger;

    public AdministratorController(
        ILogger<AdministratorController> logger,
        IUseCase<AdministratorLoginUseCaseInput, AdministratorLoginUseCaseOutput> admnistratorLoginUseCase
     )
    {
        _logger = logger;
        _admnistratorLoginUseCase = admnistratorLoginUseCase;
    }

    [HttpPost]
    [Route("Login")]
    public async Task<ObjectResult> Login([FromBody] AdministratorLoginInput userInput)
    {
       try
        {
            _logger.LogInformation(message: "{Email} made login!", userInput.Email);
            var Data = await _admnistratorLoginUseCase.Run(new AdministratorLoginUseCaseInput
            {
                Email = userInput.Email,
                Password = userInput.Password
            });
       
            return new ObjectResult(Data);
        } catch(BaseException e)
        {
            return new BadRequestObjectResult(e.Data);
        } catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}


