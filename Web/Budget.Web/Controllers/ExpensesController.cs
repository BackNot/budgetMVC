namespace Budget.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Budget.Data.Models;
    using Budget.Services.Data;
    using Budget.Web.ViewModels.Expense;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class ExpensesController : BaseController
    {
        private readonly IExpenseService expenseService;
        private readonly UserManager<ApplicationUser> userManager;

        public ExpensesController(IExpenseService expenseService, UserManager<ApplicationUser> userManager)
        {
            this.expenseService = expenseService;
            this.userManager = userManager;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddInputModel inputModel)
        {
            var userId = this.userManager.GetUserId(this.User);
            var date = DateTime.Parse(inputModel.Date);
            await this.expenseService.AddAsync(userId, inputModel.Amount, date);

            return this.Redirect("/Home/Index");
        }
    }
}
