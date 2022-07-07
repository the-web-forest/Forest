using Bonsai.Controllers.Payment.DTOS;
using Bonsai.UseCases.NewPaymentUseCase;

namespace Bonsai.Controllers.Payment.Adapters
{
	public static class NewPaymentAdapter
	{
		public static NewPaymentUseCaseInput GetUseCasePayload(NewPaymentInput Input)
        {
			return new NewPaymentUseCaseInput {
				CardToken = Input.CardToken,
				OrderId = Input.OrderId,
				Description = Input.Description,
				Value = Input.Value
			};
		}
	}
}

