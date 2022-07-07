using System.ComponentModel.DataAnnotations;

namespace Samauma.Controllers.Admnistrator;

public class AdmnistratorLoginInput
{
    [Required]
	[EmailAddress]
	public string Email { get; set; }

	[Required]
	[MinLength(8)]
	public string Password { get; set; }
}


