using System.ComponentModel.DataAnnotations;

namespace Bonsai.Controllers.Payment.DTOS
{
	public class NewPaymentInput
	{
		[Required]
		public string OrderId { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
        [Range(100, double.MaxValue)]
		public double Value { get; set; }

		[Required]
		public string CardToken { get; set; }
	}
}

