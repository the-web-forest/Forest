using System;
using Bonsai.Domain.Enums;
using Bonsai.Domain.Models;
using Bonsai.Helpers;

namespace Bonsai.UseCases.NewPaymentUseCase.Adapters
{
	public static class OrderAdapter
	{
		public static Order GetOrderModelFromInput(NewPaymentUseCaseInput Input)
        {
			return new Order
			{
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				Amount = Input.Value.ToString(),
				Description = Input.Description,
				OrderId = Input.OrderId
			};
        }
	}
}

