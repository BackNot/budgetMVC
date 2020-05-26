namespace Budget.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Budget.Data.Common.Repositories;
    using Budget.Data.Models;
    using Budget.Services.Mapping;

    public class ExpenseService : IExpenseService
    {
        private readonly IDeletableEntityRepository<Expense> expenseRepository;

        public ExpenseService(IDeletableEntityRepository<Expense> expenseRepository)
        {
            this.expenseRepository = expenseRepository;
        }

        public async Task AddAsync(string userId, decimal amount, DateTime date)
        {
            var expense = new Expense
            {
                Amount = amount,
                Date = date,
                UserId = userId,
            };

            await this.expenseRepository.AddAsync(expense);

            await this.expenseRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            this.IsExist(id);

            var expense = await this.expenseRepository.GetByIdWithDeletedAsync(id);
            expense.IsDeleted = true;
            await this.expenseRepository.SaveChangesAsync();
        }

        public async Task EditAsync(int id, decimal amount, DateTime date)
        {
            this.IsExist(id);

            var expense = await this.expenseRepository.GetByIdWithDeletedAsync(id);

            expense.Amount = amount;
            expense.Date = date;

            await this.expenseRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            var expenses = this.expenseRepository.All()
                .To<T>()
                .ToList();

            return expenses;
        }

        public T GetById<T>(int id)
        {
            this.IsExist(id);
            var expense = this.expenseRepository.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return expense;
        }

        private void IsExist(int id)
        {
            var exist = this.expenseRepository.All().Any(e => e.Id == id);

            if (exist == false)
            {
                throw new ArgumentException($"Expense  with id {id} doesn't exist!");
            }
        }
    }
}
