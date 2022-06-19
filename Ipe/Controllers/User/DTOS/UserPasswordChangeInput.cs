using System.ComponentModel.DataAnnotations;

namespace Ipe.Controllers.User.DTOS;

public class UserPasswordChangeInput
{
	[Required]
	[EmailAddress]
	public string Email { get; set; }
	[Required]
	public string Token { get; set; }
	[Required]
	[MinLength(8)]
	public string Password { get; set; }
}

