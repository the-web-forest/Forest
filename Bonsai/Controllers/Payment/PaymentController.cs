using Bonsai.Controllers.Payment.Adapters;
using Bonsai.Controllers.Payment.DTOS;
using Bonsai.Domain.Errors;
using Bonsai.UseCases;
using Bonsai.UseCases.NewPaymentUseCase;
using Microsoft.AspNetCore.Mvc;

namespace Bonsai.Controllers.Home
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : Controller
    {
        private readonly IUseCase<NewPaymentUseCaseInput, NewPaymentUseCaseOutput> _newPaymentUseCase;

        public PaymentController(
            IUseCase<NewPaymentUseCaseInput, NewPaymentUseCaseOutput> newPaymentUseCase
            )
        {
            _newPaymentUseCase = newPaymentUseCase;
        }

        [HttpPost]
        [Route("New")]
        public async Task<ObjectResult> NewPayment([FromBody] NewPaymentInput Input)
        {
            try
            {
                var UseCaseInput = NewPaymentAdapter.GetUseCasePayload(Input);
                var Response = await _newPaymentUseCase.Run(UseCaseInput);
                return new ObjectResult(Response);
            }
            catch (BaseException e)
            {
                return new BadRequestObjectResult(e.Data);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}