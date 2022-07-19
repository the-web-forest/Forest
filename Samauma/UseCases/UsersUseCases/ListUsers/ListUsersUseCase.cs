using Samauma.Domain.Errors;
using Samauma.Domain.Models;
using Samauma.UseCases.Interfaces;
using Samauma.UseCases.ListUsers.Dtos;

namespace Samauma.UseCases.ListUsers
{
    public class ListUsersUseCase : IUseCase<ListUsersUseCaseInput, ListUsersUseCaseOutput>
    {
        private readonly IUserRepository _userRepository;
        private readonly int ITENS_PER_REQUEST = 10;

        public ListUsersUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ListUsersUseCaseOutput> Run(ListUsersUseCaseInput Input)
        {
            var TotalUsers = await GetTotalUsers();
            var TotalPages = CalculatePages(TotalUsers);

            if(Input.Page > TotalPages)
            {
                throw new OutOfRangeException();
            }

            var Users = await GetUsers(Input.Page);
            var Response = BuildResponse(Input.Page, TotalPages, Users);
            return Response;
        }

        private async Task<long> GetTotalUsers()
        {
            return await _userRepository.CountUsers();
        }

        private long CalculatePages(long TotalUsers) 
        {
            if(TotalUsers < ITENS_PER_REQUEST)
            {
                return 1;
            }

            double Pages = TotalUsers / (double)ITENS_PER_REQUEST;
           
            return (long)Math.Ceiling(Pages);
        }

        private async Task<List<User>> GetUsers(int Page)
        {
            return await _userRepository.ListUsersPerPage(Page, ITENS_PER_REQUEST);
        }

        private ListUsersUseCaseOutput BuildResponse(int CurrentPage, long TotalPages, List<User> Users)
        {
            var ListOfUsers = Users.Select(x => LightUser.FromUser(x)).ToList();
            return new ListUsersUseCaseOutput
            {
                TotalPages = (int)TotalPages,
                CurrentPage = CurrentPage,
                ItemsPerPage = ITENS_PER_REQUEST,
                Users = ListOfUsers
            };
        }
    }
}

