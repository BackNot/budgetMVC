namespace Budget.Web.ViewModels.Expense
{
    using System.Collections.Generic;

    public class AllViewModel
    {
        public IEnumerable<ExpenseViewModel> Expenses { get; set; }
    }
}
