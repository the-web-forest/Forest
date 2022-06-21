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
            var User = await _userRepository.GetById(Input.UserId);

            return new GetUserInfoUseCaseOutput {
                Name = User.Name,
                Email = User.Email,
                City = User.City,
                State = User.State
            };
           
        }

    }
}