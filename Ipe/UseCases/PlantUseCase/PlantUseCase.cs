using Ipe.Domain.Errors;
using Ipe.Domain.Models;
using Ipe.UseCases.Interfaces;
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
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;

        public PlantUseCase(
            ITreeRepository treeRepository,
            IOrderRepository orderRepository,
            IPlantRepository plantRepository,
            IPaymentService paymentService,
            IEmailService emailService,
            IUserRepository userRepository
        )
        {
            _treeRepository = treeRepository;
            _orderRepository = orderRepository;
            _plantRepository = plantRepository;
            _paymentService = paymentService;
            _emailService = emailService;
            _userRepository = userRepository;
        }

        public async Task<PlantUseCaseOutput> Run(PlantUseCaseInput Input)
        {
            List<Tree> Trees = await GetTreesById(Input.Trees);
            bool InvalidTrees = HasInvalidTree(Trees);

            if (InvalidTrees)
                throw new InvalidTreeIdException();

            Order Order = await CreateOrder(Input, Trees);
            var User = await _userRepository.GetById(Input.UserId);
            var PaymentResult = await ExecutePayment(Input, Order, Trees);

            if(PaymentResult.Success)
            {
                Task.WaitAll(new Task[]
                {
                    UpdateOrderSucess(Order, PaymentResult),
                    CreatePlant(Order, Trees),
                    _emailService.SendPlantSuccessEmail(User.Email, User.Name)
            });
            }
            else
            {
                await UpdateOrderFail(Order);
                await _emailService.SendPlantFailEmail(User.Email, User.Name);
            }

            return new PlantUseCaseOutput
            {
                Planted = PaymentResult.Success
            };
        }

        private async Task UpdateOrderFail(Order order)
        {
            order.Status = PaymentStatus.DECLINED.ToString();
            await _orderRepository.Update(order);
        }

        private async Task UpdateOrderSucess(Order order, NewPaymentOutput paymentResult)
        {
            order.Status = PaymentStatus.PAID.ToString();
            order.PaymentId = paymentResult.PaymentId;
            await _orderRepository.Update(order);
        }

        private async Task CreatePlant(Order order, List<Tree> Trees)
        {
            var trees = order.Trees
                .Select(tree => new Plant
                {
                    OrderId = order.Id,
                    UserId = order.UserId,
                    TreeId = tree.TreeId,
                    Name = tree.Name,
                    Message = tree.Message,
                    Hastags = tree.Hastags
                }).ToList();

            await _plantRepository.CreateMany(trees);
        }

        private async Task<NewPaymentOutput> ExecutePayment(
            PlantUseCaseInput Input,
            Order Order,
            List<Tree> Trees
        )
        {
            double PaymentValue = GetPaymentTotalValue(Trees);

            var NewPayment = new NewPaymentInput
            {
                OrderId = $"IPE#{Order.Id}#{Input.UserId}",
                CardToken = Input.CardToken,
                Description = "Plant Payment",
                Value = PaymentValue
            };

            return await _paymentService.NewPayment(NewPayment);
        }

        private static double GetPaymentTotalValue(List<Tree> trees)
        {
            return trees.Sum(tree => tree.Value);
        }

        private async Task<Order> CreateOrder(PlantUseCaseInput Input, List<Tree> Trees)
        {
            var OrderValue = GetPaymentTotalValue(Trees);
            var order = new Order
            {
                Status = PaymentStatus.CREATED.ToString(),
                UserId = Input.UserId,
                Trees = Input.Trees
                    .Select(tree => new OrderTree
                    {
                        TreeId = tree.Id,
                        Name = tree.Name,
                        Message = tree.Message,
                        Hastags = tree.Hastags
                    })
                    .ToList(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                PaymentId = null,
                Value = OrderValue
            };

            await _orderRepository.Create(order);

            return order;
        }

        private async Task<List<Tree>> GetTreesById(List<TreeUseCaseInput> trees)
        {
            var TreesId = trees.Select(t => t.Id).ToList();
            var AllTrees = await _treeRepository.GetTreesById(TreeId: TreesId);
            return AllTrees.ToList();
        }

        private static bool HasInvalidTree(List<Tree> Trees)
        {
            if (Trees is null || !Trees.Any())
                return true;

            return Trees.Any(tree => tree.Deleted == true);
        }

      
    }
}
