using System;
namespace Ipe.UseCases.Interfaces.Services
{
    public interface IPaymentService
    {
        public Task<NewPaymentOutput> NewPayment(NewPaymentInput Input);
    }
}

