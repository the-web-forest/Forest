
namespace Ipe.UseCases.Interfaces
{
	public interface IEmailService
	{
		Task<bool> SendWelcomeEmail(string Email, string Name, string Token, string Role);
		Task<bool> SendPasswordResetEmail(string Email, string Name, string Token, string Role);
	}
}