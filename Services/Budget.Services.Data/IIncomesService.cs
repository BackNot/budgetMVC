using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Services.Data
{
    public interface IIncomesService
    {
        Task AddAsync(string userId, decimal amount, DateTime date, bool isRepeating);

        Task EditAsync(int id, decimal amount, DateTime date);

        Task DeleteAsync(int id);

        T GetById<T>(int id);

        IEnumerable<T> GetAll<T>();
    }
}
