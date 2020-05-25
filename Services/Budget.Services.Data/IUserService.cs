namespace Budget.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<decimal> GetSum(string id);
    }
}
