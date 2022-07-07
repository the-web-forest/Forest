using System.ComponentModel.DataAnnotations;

namespace Ipe.Controllers.Email.DTOS;

public class SendEmailInput
{
	[Required]
	[EmailAddress]
	public string Email { get; set; }
}

