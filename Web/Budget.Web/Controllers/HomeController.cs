namespace Budget.Web.Controllers
{
    using System.Diagnostics;
    using System.Security.Claims;
    using Budget.Data;
    using Budget.Data.Models;
    using Budget.Web.ViewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        ApplicationDbContext context;
        UserManager<ApplicationUser> userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            var userId = userManager.GetUserId(User);

            var incomes = context.Incomes;
            var expenses = context.Expenses;
            decimal incomeSum = 0;
            foreach (var income in incomes)
            {
                if (income.UserId == userId)
                    incomeSum += income.Amount;
            }

            decimal expenseSum = 0;
            foreach (var expense in expenses)
            {
                if (expense.UserId == userId)
                    expenseSum += expense.Amount;
            }

            this.ViewBag.expenses = expenseSum;
            this.ViewBag.incomes = incomeSum;
            this.ViewBag.total = incomeSum - expenseSum;
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
