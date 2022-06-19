using Bonsai.Domain.Errors;
using Bonsai.Domain.Models;
using Bonsai.UseCases.Interfaces;
using Bonsai.UseCases.Interfaces.Services;
using Bonsai.UseCases.Interfaces.Services.Payment;
using Bonsai.UseCases.NewPaymentUseCase.Adapters;

namespace Bonsai.UseCases.NewPaymentUseCase
{
	public class NewPaymentUseCase: IUseCase<NewPaymentUseCaseInput, NewPaymentUseCaseOutput>
	{
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentService _paymentService;

        public NewPaymentUseCase(
            IOrderRepository orderRepository,
            IPaymentService paymentService
            ) {
            _orderRepository = orderRepository;
            _paymentService = paymentService;
        }

		public async Task<NewPaymentUseCaseOutput> Run(NewPaymentUseCaseInput Input)
        {
            var Model = OrderAdapter.GetOrderModelFromInput(Input);

            await _orderRepository.Create(Model);

            var PaymentResponse = await _paymentService.Pay(Input, Model);

            Model = await UpdatePaymentStatus(Model, PaymentResponse);

            if(!PaymentResponse.IsPaymentSuccess)
            {
                throw new PaymentDeniedException();
            }

            return new NewPaymentUseCaseOutput {
               PaymentId = Model.Id.ToString(),
               PaymentStatus = Model.PaymentStatus
            };
        }

        private async Task<Order> UpdatePaymentStatus(Order Model, PaymentOutput PaymentOutput)
        {
            Model.PaymentRequest = PaymentOutput.PaymentRequest;
            Model.PaymentResponse = PaymentOutput.PaymentResponse;
            Model.PaymentStatus = PaymentOutput.PaymentStatus;
            Model.UpdatedAt = DateTime.Now;
            await _orderRepository.Update(Model);
            return Model;
        }

     
    }
}

