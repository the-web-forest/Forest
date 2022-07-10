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
  
    public AdministratorController(
        IUseCase<AdministratorLoginUseCaseInput, AdministratorLoginUseCaseOutput> admnistratorLoginUseCase
     )
    {
        _admnistratorLoginUseCase = admnistratorLoginUseCase;
    }

    [HttpPost]
    [Route("Login")]
    public async Task<ObjectResult> Login([FromBody] AdministratorLoginInput userInput)
    {
       try
        {
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
            return new BadRequestObjectResult(e.Message);
        }
    }
}


