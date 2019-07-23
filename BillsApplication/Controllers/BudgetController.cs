using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BillsApplication.Data;
using BillsData;
using BillsApplication.Services.Budget;
using BillsApplication.Services;

namespace BillsApplication.Controllers
{
    public class BudgetController : Controller
    {
        private readonly IBudget _budgetService;
        private readonly ICategory _categoryService;

        public BudgetController(IBudget budgetService, ICategory categoryService)
        {
            _budgetService = budgetService;
            _categoryService = categoryService;
        }

        // GET: Budget
        public IActionResult Index()
        {
            var budget = _budgetService.GetAll().Include(b => b.TransactionCategory).ToList();
            return View(budget);
        }

        // GET: Budget/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var budget = _budgetService.GetAll()
                .Include(b => b.TransactionCategory)
                .FirstOrDefault(m => m.Id == id);
            if (budget == null)
            {
                return NotFound();
            }

            return View(budget);
        }

        // GET: Budget/Create
        public IActionResult Create()
        {
            ViewData["TransactionCategoryId"] = _categoryService.GetTransactionCategories();
            return View();
        }

        // POST: Budget/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( Budget budget,Transaction transaction)
        {
            if (ModelState.IsValid)
            {

                _budgetService.Add(budget);
                _budgetService.SetBudgetAmount(budget);

                return RedirectToAction(nameof(Index));
            }
            ViewData["TransactionCategoryId"] = _categoryService.GetTransactionCategories();
            return View(budget);
        }

        // GET: Budget/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _budgetService.GetAll().FindAsync(id);
            if (budget == null)
            {
                return NotFound();
            }
            ViewData["TransactionCategoryId"] = _categoryService.GetTransactionCategories();
            return View(budget);
        }

        // POST: Budget/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Name,TransactionCategoryId,Amount,From,To,CreateDate,ModyficationDate,UserId,Id")] Budget budget)
        {
            if (id != budget.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _budgetService.EditBudget(budget);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BudgetExists(budget.Id))
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
            return View(budget);
        }
        // GET: Budget/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = _budgetService.GetAll()
                .Include(b => b.TransactionCategory)
                .FirstOrDefault(m => m.Id == id);
            if (budget == null)
            {
                return NotFound();
            }

            return View(budget);
        }

        // POST: Budget/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _budgetService.DeleteBudget(id);
            return RedirectToAction(nameof(Index));
        }

        private bool BudgetExists(int id)
        {
            return _budgetService.GetAll().Any(e => e.Id == id);
        }
    }
}
