using System.Security.Claims;
using Samauma.Domain.Errors;
using Samauma.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Samauma.UseCases.AdmnistratorLogin;

namespace Samauma.Controllers.Admnistrator;

[ApiController]
[Route("[controller]")]
public class AdmnistratorController : Controller
{

    private readonly IUseCase<AdmnistratorLoginUseCaseInput, AdmnistratorLoginUseCaseOutput> _admnistratorLoginUseCase;
  
    public AdmnistratorController(
        IUseCase<AdmnistratorLoginUseCaseInput, AdmnistratorLoginUseCaseOutput> admnistratorLoginUseCase
     )
    {
        _admnistratorLoginUseCase = admnistratorLoginUseCase;
    }

    [HttpPost]
    [Route("Login")]
    public async Task<ObjectResult> Login([FromBody] AdmnistratorLoginInput userInput)
    {
       try
        {
            var Data = await _admnistratorLoginUseCase.Run(new AdmnistratorLoginUseCaseInput
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


