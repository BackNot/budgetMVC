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

namespace Budget.Web.Controllers
{
    [Authorize]
    public class IncomesController : Controller
    {
        private readonly IIncomesService incomesService;
        private readonly UserManager<ApplicationUser> userManager;

        public IncomesController(IIncomesService incomesService, UserManager<ApplicationUser> userManager)
        {
            this.incomesService = incomesService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
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
            await this.incomesService.AddAsync(userId, inputModel.Amount, date, inputModel.IsRepeating);

            return this.Redirect("/Home/Index");
        }

    }
}