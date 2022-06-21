using Ipe.Domain.Exceptions;
using Ipe.Domain.Models;
using Ipe.UseCases.Interfaces;

namespace Ipe.UseCases.GetUserInfo
{
    public class GetUserInfoUseCase : IUseCase<GetUserInfoUseCaseInput, GetUserInfoUseCaseOutput>
    {
        private readonly IUserRepository _userRepository;

        public GetUserInfoUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserInfoUseCaseOutput> Run(GetUserInfoUseCaseInput Input)
        {
            try
            {
                var User = await _userRepository.GetById(Input.UserId);

                if (User is null)
                {
                    throw new InvalidUserIdException();
                }

                return BuildResponse(User);
               
            } catch (Exception)
            {
                throw new InvalidUserIdException();
            }
        }

        private static GetUserInfoUseCaseOutput BuildResponse(User User)
        {
            return new GetUserInfoUseCaseOutput
            {
                Name = User.Name,
                Email = User.Email,
                City = User.City,
                State = User.State
            };
        }

    }
}