using System;
using Samauma.Domain.Errors;
using Samauma.Domain.Models;
using Samauma.UseCases.Interfaces;

namespace Samauma.UseCases.UserDetail
{
	public class UserDetailUseCase: IUseCase<UserDetailUseCaseInput, UserDetailUseCaseOutput>
	{
        public readonly IUserRepository _userRepository;

		public UserDetailUseCase(IUserRepository userRepository)
		{
            _userRepository = userRepository;
		}

        public async Task<UserDetailUseCaseOutput> Run(UserDetailUseCaseInput Input)
        {
            var User = await GetUserById(Input.Id);

            if(User is null)
            {
                throw new InvalidUserIdException();
            }

            return BuildOutput(User);
        }

        private async Task<User> GetUserById(string UserId)
        {
            try
            {
                return await _userRepository.GetById(UserId);
            } catch 
            {
                throw new InvalidUserIdException();
            }
        }

        private UserDetailUseCaseOutput BuildOutput(User User)
        {
            return new UserDetailUseCaseOutput
            {
                Id = User.Id,
                Name = User.Name,
                City = User.City,
                State = User.State,
                Email = User.Email,
                CreatedAt = User.CreatedAt,
                UpdatedAt = User.UpdatedAt,
                EmailVerified = User.EmailVerified
            };
        }
    }
}

