using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BillsApplication.Data;
using BillsData;
using BillsApplication.Services;
using Microsoft.AspNetCore.Authorization;

namespace BillsApplication.Controllers
{
    [Authorize]
    public class TransactionCategoryController : Controller
    {
        private readonly ICategory _categoryService;

        public TransactionCategoryController(ICategory categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: TransactionCategories
        public IActionResult Index()
        {
            return View(_categoryService.GetAll());
        }

        // GET: TransactionCategories/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionCategory = _categoryService.GetAll()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transactionCategory == null)
            {
                return NotFound();
            }

            return View(transactionCategory);
        }

        // GET: TransactionCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TransactionCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Id")] TransactionCategory transactionCategory)
        {
            if (ModelState.IsValid)
            {
                 _categoryService.Add(transactionCategory);
                return RedirectToAction(nameof(Index));
            }
            return View(transactionCategory);
        }

        // GET: TransactionCategories/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionCategory =  _categoryService.GetAll().FindAsync(id);
            if (transactionCategory == null)
            {
                return NotFound();
            }
            return View(transactionCategory);
        }

        // POST: TransactionCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Name,Id")] TransactionCategory transactionCategory)
        {
            if (id != transactionCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   _categoryService.EditCategory(transactionCategory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionCategoryExists(transactionCategory.Id))
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
            return View(transactionCategory);
        }

        // GET: TransactionCategories/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionCategory = _categoryService.GetAll()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transactionCategory == null)
            {
                return NotFound();
            }

            return View(transactionCategory);
        }

        // POST: TransactionCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _categoryService.DeleteCategory(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionCategoryExists(int id)
        {
            return _categoryService.GetAll().Any(e => e.Id == id);
        }
    }
}
