using System;
using Bonsai.Domain.Models;
using Bonsai.UseCases.Interfaces.Services.Payment;
using Bonsai.UseCases.NewPaymentUseCase;

namespace Bonsai.UseCases.Interfaces.Services
{
	public interface IPaymentService
	{
		Task<PaymentOutput> Pay(NewPaymentUseCaseInput Input, Order Order);
	}
}

