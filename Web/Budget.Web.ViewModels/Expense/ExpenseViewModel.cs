namespace Budget.Web.ViewModels.Expense
{
    using Budget.Services.Mapping;
    using Budget.Data.Models;
    using System;

    public class ExpenseViewModel : IMapFrom<Expense>
    {
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
    }
}
