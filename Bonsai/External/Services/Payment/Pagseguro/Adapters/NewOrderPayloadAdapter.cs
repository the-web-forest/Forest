using System;
using Bonsai.Domain.Enums;
using Bonsai.Domain.Models;
using Bonsai.External.Services.Payment.Pagseguro.DTOS;
using Bonsai.UseCases.NewPaymentUseCase;

namespace Bonsai.External.Services.Payment.Pagseguro.Adapters
{
	public static class NewOrderPayloadAdapter
	{
		public static NewOrderRequestPayload BuildPayload(NewPaymentUseCaseInput Input, Order Order)
        {
			return new NewOrderRequestPayload
			{
				ReferenceId = Order.Id.ToString(),
				Description = Input.Description,
				Amount = new NewOrderRequestPayload.AmountData
                {
					Value = Input.Value,
					Currency = "BRL"
                },
				PaymentMethod = new NewOrderRequestPayload.PaymentMethodData
                {
					Type = "CREDIT_CARD",
					Capture = true,
					Installments = 1,
					Card = new NewOrderRequestPayload.PaymentMethodCardData
                    {
						Encrypted = Input.CardToken
                    }
                }

			};
        }
	}
}

