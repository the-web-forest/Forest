using System.ComponentModel.DataAnnotations;

namespace Ipe.Controllers.Email.DTOS;

public class ValidateEmailInput
{
	[Required]
	[EmailAddress]
	public string Email { get; set; }

	[Required]
	public string Token { get; set; }

	[Required]
	public string Role { get; set; }
}
