using System.ComponentModel.DataAnnotations;

namespace Ipe.Controllers.User;

public class UserLoginInput
{
    [Required]
	[EmailAddress]
	public string Email { get; set; }

	[Required]
	[MinLength(8)]
	public string Password { get; set; }
}


