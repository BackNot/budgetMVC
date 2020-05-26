using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Budget.Data;
using Budget.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace Budget.Web.Controllers
{
    [Authorize]
    public class EditIncomesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EditIncomesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EditIncomes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Incomes.Include(i => i.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EditIncomes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var income = await _context.Incomes
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (income == null)
            {
                return NotFound();
            }

            return View(income);
        }

        // GET: EditIncomes/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: EditIncomes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Amount,Date,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Income income)
        {
            if (ModelState.IsValid)
            {
                _context.Add(income);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", income.UserId);
            return View(income);
        }

        // GET: EditIncomes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var income = await _context.Incomes.FindAsync(id);
            if (income == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", income.UserId);
            return View(income);
        }

        // POST: EditIncomes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Amount,Date,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn,IsRepeating")] Income income)
        {
            if (id != income.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(income);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncomeExists(income.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", income.UserId);
            return View(income);
        }

        // GET: EditIncomes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var income = await _context.Incomes
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (income == null)
            {
                return NotFound();
            }

            return View(income);
        }

        // POST: EditIncomes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var income = await _context.Incomes.FindAsync(id);
            _context.Incomes.Remove(income);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncomeExists(int id)
        {
            return _context.Incomes.Any(e => e.Id == id);
        }
    }
}
