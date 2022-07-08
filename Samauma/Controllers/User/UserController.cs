using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Samauma.Domain.Errors;
using Samauma.UseCases;
using Samauma.UseCases.ListUsers;

namespace Samauma.Controllers.User
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
	{

        private readonly IUseCase<ListUsersUseCaseInput, ListUsersUseCaseOutput> _listUsersUseCase;

        public UserController(
            IUseCase<ListUsersUseCaseInput, ListUsersUseCaseOutput> listUsersUseCase
            )
		{
            _listUsersUseCase = listUsersUseCase;
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
    }
}

