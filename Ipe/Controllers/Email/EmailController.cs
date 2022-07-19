using Ipe.Controllers.Email.DTOS;
using Ipe.Controllers.User.DTOS;
using Ipe.Domain.Errors;
using Ipe.UseCases;
using Ipe.UseCases.CheckEmail;
using Ipe.UseCases.SendVerificationEmail;
using Ipe.UseCases.UserPasswordReset;
using Ipe.UseCases.ValidateEmail;
using Microsoft.AspNetCore.Mvc;

namespace Ipe.Controllers.Email;

[ApiController]
[Route("[controller]")]
public class EmailController : Controller
{

    private readonly IUseCase<ValidateEmailUseCaseInput, ValidateEmailUseCaseOutput> _validateEmailUseCase;
    private readonly IUseCase<CheckEmailUseCaseInput, CheckEmailUseCaseOutput> _checkEmailUseCase;
    private readonly IUseCase<SendVerificationEmailUseCaseInput, SendVerificationEmailUseCaseOutput> _sendVerificationEmailUseCase;
    private readonly IUseCase<UserPasswordResetUseCaseInput, UserPasswordResetUseCaseOutput> _userPasswordResetUseCase;

    public EmailController(
        IUseCase<ValidateEmailUseCaseInput, ValidateEmailUseCaseOutput> validateEmailUseCase,
        IUseCase<CheckEmailUseCaseInput, CheckEmailUseCaseOutput> checkEmailUseCase,
        IUseCase<SendVerificationEmailUseCaseInput, SendVerificationEmailUseCaseOutput> sendVerificationEmailUseCase,
        IUseCase<UserPasswordResetUseCaseInput, UserPasswordResetUseCaseOutput> userPasswordResetUseCase
     )
    {
        _validateEmailUseCase = validateEmailUseCase;
        _checkEmailUseCase = checkEmailUseCase;
        _sendVerificationEmailUseCase = sendVerificationEmailUseCase;
        _userPasswordResetUseCase = userPasswordResetUseCase;
    }

    [HttpPost]
    [Route("Send/Validation")]
    public async Task<ObjectResult> Validate([FromBody] SendEmailInput Input)
    {
        try
        {
            var Data = await _sendVerificationEmailUseCase.Run(new SendVerificationEmailUseCaseInput
            {
                Email = Input.Email
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


    [HttpPost]
    [Route("Validate")]
    public async Task<ObjectResult> Validate([FromBody] ValidateEmailInput Input)
    {
        try
        {
            var Data = await _validateEmailUseCase.Run(new ValidateEmailUseCaseInput {
                Email = Input.Email,
                Token = Input.Token,
                Role = Input.Role
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

    [HttpGet]
    [Route("Check")]
    public async Task<ObjectResult> Check(
        [FromQuery(Name = "email")] string Email)
    {
        try
        {
            var Data = await _checkEmailUseCase.Run(new CheckEmailUseCaseInput
            {
                Email = Email
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

    [HttpPost]
    [Route("Send/PasswordReset")]
    public async Task<ObjectResult> PasswordReset([FromBody] UserPasswordResetInput Input)
    {
        try
        {
            var data = await _userPasswordResetUseCase.Run(new UserPasswordResetUseCaseInput
            {
                Email = Input.Email
            });

            return new OkObjectResult(data);
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

