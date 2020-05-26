using Budget.Data.Common.Repositories;
using Budget.Data.Models;
using Budget.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Services.Data
{
    public class IncomesService : IIncomesService
    {
        private readonly IDeletableEntityRepository<Income> incomeRepository;

        public IncomesService(IDeletableEntityRepository<Income> incomeRepository)
        {
            this.incomeRepository = incomeRepository;
        }

        public async Task AddAsync(string userId, decimal amount, DateTime date, bool isRepeating = false)
        {
            var income = new Income
            {
                Amount = amount,
                Date = date,
                UserId = userId,
                IsRepeating = isRepeating
            };

            await this.incomeRepository.AddAsync(income);

            await this.incomeRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            this.IsExist(id);

            var expense = await this.incomeRepository.GetByIdWithDeletedAsync(id);
            expense.IsDeleted = true;
            await this.incomeRepository.SaveChangesAsync();
        }

        public async Task EditAsync(int id, decimal amount, DateTime date)
        {
            this.IsExist(id);

            var expense = await this.incomeRepository.GetByIdWithDeletedAsync(id);

            expense.Amount = amount;
            expense.Date = date;

            await this.incomeRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            var expenses = this.incomeRepository.All()
                .To<T>()
                .ToList();

            return expenses;
        }

        public T GetById<T>(int id)
        {
            this.IsExist(id);
            var expense = this.incomeRepository.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return expense;
        }

        private void IsExist(int id)
        {
            var exist = this.incomeRepository.All().Any(e => e.Id == id);

            if (exist == false)
            {
                throw new ArgumentException($"Expense  with id {id} doesn't exist!");
            }
        }
}
}
