namespace Budget.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Budget.Data.Common.Repositories;
    using Budget.Data.Models;

    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public UserService(IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<decimal> GetSum(string id)
        {
            var user = await this.userRepository.GetByIdWithDeletedAsync(id);
            var sum = user.Incomes.Sum(i => i.Amount) - user.Expenses.Sum(e => e.Amount);

            return sum;
        }
    }
}
