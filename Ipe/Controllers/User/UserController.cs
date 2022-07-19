using System.Security.Claims;
using Ipe.Controllers.User.DTOS;
using Ipe.Domain.Errors;
using Ipe.UseCases;
using Ipe.UseCases.GetUserInfo;
using Ipe.UseCases.Login;
using Ipe.UseCases.Register;
using Ipe.UseCases.UserPasswordChange;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ipe.Controllers.User;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{

    private readonly IUseCase<LoginUseCaseInput, LoginUseCaseOutput> _loginUseCase;
    private readonly IUseCase<UserRegisterUseCaseInput, UserRegisterUseCaseOutput> _registerUseCase;
    private readonly IUseCase<UserPasswordChangeUseCaseInput, UserPasswordChangeUseCaseOutput> _userPasswordChangeUseCase;
    private readonly IUseCase<GetUserInfoUseCaseInput, GetUserInfoUseCaseOutput> _getUserInfoUseCase;

    public UserController(
        IUseCase<LoginUseCaseInput, LoginUseCaseOutput> loginUseCase,
        IUseCase<UserRegisterUseCaseInput, UserRegisterUseCaseOutput> registerUseCase,
        IUseCase<UserPasswordChangeUseCaseInput, UserPasswordChangeUseCaseOutput> userPasswordChangeUseCase,
        IUseCase<GetUserInfoUseCaseInput, GetUserInfoUseCaseOutput> getUserInfoUseCase
        )
    {
        _loginUseCase = loginUseCase;
        _registerUseCase = registerUseCase;
        _userPasswordChangeUseCase = userPasswordChangeUseCase;
        _getUserInfoUseCase = getUserInfoUseCase;
    }

    [HttpGet]
    [Authorize]
    public async Task<ObjectResult> GetUserInfo()
    {
        try
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var UseCaseInput = new GetUserInfoUseCaseInput
            {
                UserId = UserId
            };
            var Data = await _getUserInfoUseCase.Run(UseCaseInput);
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
    [Route("Login")]
    public async Task<ObjectResult> Login([FromBody] UserLoginInput userInput)
    {
       try
        {
            var Data = await _loginUseCase.Run(new LoginUseCaseInput
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

    [HttpPost]
    public async Task<ObjectResult> Register([FromBody] UserRegisterInput UserRegisterInput)
    {
        try
        {
            var data = await _registerUseCase.Run(new UserRegisterUseCaseInput
            {
                Name = UserRegisterInput.Name,
                Email = UserRegisterInput.Email,
                State = UserRegisterInput.State,
                City = UserRegisterInput.City,
                Password = UserRegisterInput.Password
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

    [HttpPost]
    [Route("Password/Change")]
    public async Task<ObjectResult> PasswordChange([FromBody] UserPasswordChangeInput Input)
    {
        try
        {
            var data = await _userPasswordChangeUseCase.Run(new UserPasswordChangeUseCaseInput
            {
                Email = Input.Email,
                Token = Input.Token,
                Password = Input.Password
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


