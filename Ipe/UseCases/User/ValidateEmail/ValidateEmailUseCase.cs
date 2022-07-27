using Ipe.Domain.Errors;
using Ipe.Domain.Models;
using Ipe.UseCases.Interfaces;

namespace Ipe.UseCases.ValidateEmail
{
	public class ValidateEmailUseCase: IUseCase<ValidateEmailUseCaseInput, ValidateEmailUseCaseOutput>
	{
		private readonly IMailVerificationRepository _mailVerificationRepository;
		private readonly IUserRepository _userRepository;

		public ValidateEmailUseCase(
			IMailVerificationRepository mailVerificationRepository,
			IUserRepository userRepository
			)
		{
			_mailVerificationRepository = mailVerificationRepository;
			_userRepository = userRepository;
		}

        public async Task<ValidateEmailUseCaseOutput> Run(ValidateEmailUseCaseInput Input)
        {
			var LastRegister = _mailVerificationRepository.GetLastRegisterByEmail(Input.Email);
            ValidateMailVerificationAttempt(LastRegister, Input);
			await UpdateMailVerificationRegister(LastRegister);
			await UpdateUserVerificationEmailStatus(LastRegister.Email);
			return new ValidateEmailUseCaseOutput();

        }

		private static void ValidateMailVerificationAttempt(MailVerification MailVerification, ValidateEmailUseCaseInput Input)
        {
			if (MailVerification == null)
            {
                throw new InvalidEmailValidationException();
            }

            if (MailVerification.Activated == true)
            {
                throw new InvalidEmailValidationException();
            }

            if (MailVerification.Role != Input.Role)
            {
                throw new InvalidEmailValidationException();
            }

            if (MailVerification.Token != Input.Token)
            {
                throw new InvalidEmailValidationException();
            }

            DateTime Now = DateTime.Now;
			DateTime StartTime = MailVerification.CreatedAt;
			DateTime EndTime = StartTime.AddHours(24);

			if(Now > EndTime)
				throw new InvalidEmailValidationException();

		}

		private async Task UpdateMailVerificationRegister(MailVerification MailVerification)
        {
			MailVerification.Activated = true;
			await _mailVerificationRepository.Update(MailVerification);
		}

		private async Task UpdateUserVerificationEmailStatus(string Email)
        {
			var UserToUpdate = await _userRepository.GetByEmail(Email);
			UserToUpdate.EmailVerified = true;
			await _userRepository.Update(UserToUpdate);
        }
    }
}

