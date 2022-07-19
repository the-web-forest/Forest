using Ipe.Domain.Errors;
using Ipe.UseCases.Interfaces;
using System.ComponentModel.DataAnnotations;


namespace Ipe.UseCases.CheckEmail
{
	public class CheckEmailUseCase : IUseCase<CheckEmailUseCaseInput, CheckEmailUseCaseOutput>
	{
		private readonly IUserRepository _userRepository;

		public CheckEmailUseCase(IUserRepository userRepository) {
            _userRepository = userRepository;
		}

		public async Task<CheckEmailUseCaseOutput> Run(CheckEmailUseCaseInput Input)
		{

			if (new EmailAddressAttribute().IsValid(Input.Email) is false)
				throw new InvalidEmailValidationException();

			var UserExists = await _userRepository.GetByEmail(Input.Email);

			if (UserExists is not null)
				throw new EmailAlreadyRegisteredException();

			return new CheckEmailUseCaseOutput
			{
				Status = "Available"
            };
		}

	}
}