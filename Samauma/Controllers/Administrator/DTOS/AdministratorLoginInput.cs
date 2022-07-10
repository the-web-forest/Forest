using System.ComponentModel.DataAnnotations;

namespace Samauma.Controllers.Administrator;

public class AdministratorLoginInput
{
    [Required]
	[EmailAddress]
	public string Email { get; set; }

	[Required]
	[MinLength(8)]
	public string Password { get; set; }
}


