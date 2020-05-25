namespace Budget.Services.Data
{
    using System;
    using System.Threading.Tasks;

    public interface IExpenseService
    {
        Task AddAsync(string userId, decimal amount, DateTime date);

        Task EditAsync(int id, decimal amount, DateTime date);

        Task DeleteAsync(int id);
    }
}
