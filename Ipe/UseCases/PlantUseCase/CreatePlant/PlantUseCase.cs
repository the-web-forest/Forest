using Ipe.Domain.Errors;
using Ipe.Domain.Models;
using Ipe.UseCases.Interfaces.Repositories;
using Ipe.UseCases.Interfaces.Services;

namespace Ipe.UseCases.PlantUseCase.CreatePlant
{
    public class PlantUseCase : IUseCase<PlantUseCaseInput, PlantUseCaseOutput>
    {
        private readonly ITreeRepository _treeRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IPlantRepository _plantRepository;
        private readonly IPaymentService _paymentService;

        public PlantUseCase(
            ITreeRepository treeRepository,
            IOrderRepository orderRepository,
            IPlantRepository plantRepository,
            IPaymentService paymentService
        )
        {
            _treeRepository = treeRepository;
            _orderRepository = orderRepository;
            _plantRepository = plantRepository;
            _paymentService = paymentService;
        }

        public async Task<PlantUseCaseOutput> Run(PlantUseCaseInput Input)
        {
            List<Tree> trees = await GetTreesById(Input.Trees);
            bool hasInvalidTree = HasInvalidTree(trees);

            if (hasInvalidTree)
                throw new InvalidTreeIdException();

            Order order = await InserirOrder(Input);
            var paymentResult = await ExecutePayment(Input, order, trees);

            if(paymentResult.Success)
            {
                Task.WaitAll(new Task[]
                {
                    UpdateOrderSucess(order, paymentResult),
                    CreatePlant(order)
                });
            }
            else
            {
                await UpdateOrderFail(order);
            }

            //disparar email

            return new PlantUseCaseOutput
            {
                Planted = paymentResult.Success
            };
        }

        private async Task UpdateOrderFail(Order order)
        {
            order.Status = Status.Declined.ToString();
            await _orderRepository.Update(order);
        }

        private async Task UpdateOrderSucess(Order order, NewPaymentOutput paymentResult)
        {
            order.Status = Status.Paid.ToString();
            order.PaymentId = paymentResult.PaymentId;
            await _orderRepository.Update(order);
        }

        private async Task CreatePlant(Order order)
        {
            var trees = order.Trees
                .Select(tree => new Plant
                {
                    OrderId = order.Id,
                    TreeId = tree.TreeId,
                    Name = tree.Name,
                    Message = tree.Message,
                    Hastags = tree.Hastags
                });


            await _plantRepository.CreateMany(trees);
        }

        private async Task<NewPaymentOutput> ExecutePayment(
            PlantUseCaseInput Input,
            Order Order,
            List<Tree> Trees
        )
        {
            double paymentValue = GetPaymentTotalValue(Trees);
            var newPayment = new NewPaymentInput
            {
                OrderId = Order.Id,
                CardToken = Input.CardToken,
                Description = "Plant Payment",
                Value = paymentValue
            };

            return await _paymentService.NewPayment(newPayment);
        }

        private double GetPaymentTotalValue(List<Tree> trees)
        {
            return trees.Sum(tree => tree.Value);
        }

        private async Task<Order> InserirOrder(PlantUseCaseInput Input)
        {
            var order = new Order
            {
                Status = Status.Created.ToString(),
                UserId = Input.UserId,
                Trees = Input.Trees
                    .Select(tree => new OrderTree
                    {
                        TreeId = tree.Id,
                        Name = tree.Name,
                        Message = tree.Message,
                        Hastags = tree.Hastags
                    })
                    .ToList()
            };

            await _orderRepository.Create(order);

            return order;
        }

        private async Task<List<Tree>> GetTreesById(List<TreeUseCaseInput> trees)
        {
            var tressId = trees.Select(t => t.Id).ToList();
            return (await _treeRepository.GetTreesById(TreeId: tressId)).ToList();
        }

        private bool HasInvalidTree(List<Tree> trees)
        {
            if (trees is null || !trees.Any())
                return true;

            return trees.Any(tree => tree.Deleted == true);
        }
    }
}
