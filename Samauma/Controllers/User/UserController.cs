using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Samauma.Domain.Errors;
using Samauma.UseCases;
using Samauma.UseCases.ListUsers;
using Samauma.UseCases.UserDetail;

namespace Samauma.Controllers.User
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
	{

        private readonly IUseCase<ListUsersUseCaseInput, ListUsersUseCaseOutput> _listUsersUseCase;
        private readonly IUseCase<UserDetailUseCaseInput, UserDetailUseCaseOutput> _userDetailUseCase;

        public UserController(
            IUseCase<ListUsersUseCaseInput, ListUsersUseCaseOutput> listUsersUseCase,
            IUseCase<UserDetailUseCaseInput, UserDetailUseCaseOutput> userDetailUseCase
            )
		{
            _listUsersUseCase = listUsersUseCase;
            _userDetailUseCase = userDetailUseCase;
		}

        [HttpGet]
        [Route("List")]
        [Authorize]
        public async Task<ObjectResult> List([FromQuery(Name = "Page")] int Page)
        {

            if(Page < 1)
            {
                Page = 1;
            }

            try
            {
                var Data = await _listUsersUseCase.Run(new ListUsersUseCaseInput
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

        [HttpGet]
        [Route("{UserId}")]
        [Authorize]
        public async Task<ObjectResult> UserDetails(string UserId)
        {
            try
            {
                var Data = await _userDetailUseCase.Run(new UserDetailUseCaseInput
                {
                    Id = UserId
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

