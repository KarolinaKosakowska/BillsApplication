using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BillsApplication.Data;
using BillsData;
using BillsApplication.Models.TransactionForm;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using BillsApplication.Services;

namespace BillsApplication.Controllers
{
    public class TransactionController : Controller
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransaction _transactionService;
        private readonly ICategory _categoryService;
        private readonly IPaymentType _paymentTypeService;

        public TransactionController(ITransaction transactionService, ICategory categoryService, IPaymentType paymentTypeService)
        {
            _transactionService = transactionService;
            _categoryService = categoryService;
            _paymentTypeService = paymentTypeService;
        }

        // GET: Transaction
        public IActionResult Index()
        {
            var assetModels = _transactionService.GetAll();
            var listingResult = assetModels
                .Select(result => new TransactionListingModel
                {
                    Id = result.Id,
                    TransactionCategory = _transactionService.GetCategory(result.Id),
                    Name = result.Name,
                    Description = result.Description,
                    TransactionDate = result.TransactionDate,
                    Price = result.Price,
                    PaymentType = _transactionService.GetPaymentType(result.Id)

                });
            var model = new TransactionIndexModel()
            {
                TransactionsListingModels = listingResult
            };
            return View(model);
        }


        // GET: Transaction/Details/5
        public IActionResult Details(int id)
        {
            var asset = _transactionService.GetById(id);
            var model = new DetailsModel
            {
                Id = id,
                TransactionCategory = _transactionService.GetCategory(id),
                Name = asset.Name,
                Description = asset.Description,
                TransactionDate = asset.TransactionDate,
                Price = asset.Price,
                PaymentType = _transactionService.GetPaymentType(id),
                CreationDate = DateTime.Now,
                ModyficationDate = asset.ModyficationDate,
                TransactionTags = _transactionService.GetTransactionTag(id),
                Product = _transactionService.GetProduct(id),
                Amount = _transactionService.GetAmout(id),
          
            };
            return View(model);
        }

        // GET: Transaction/Create
        public IActionResult Create()
        {
            ViewData["PaymentTypeId"] =_paymentTypeService.GetPaymentTypes();
            ViewData["TransactionCategoryId"] = _categoryService.GetTransactionCategories();
            return View();
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateModel transaction)
        { 
            if (ModelState.IsValid)
            {
               // transaction.UserID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                _transactionService.Add(transaction);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentTypeId"] = _paymentTypeService.GetPaymentTypes();
            ViewData["TransactionCategoryId"] = _categoryService.GetTransactionCategories(); 
            return View(transaction);
        }
    }

    //    // GET: Transaction/Edit/5
    //    public async Task<IActionResult> Edit(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var transaction = await _context.Transactions.FindAsync(id);
    //        if (transaction == null)
    //        {
    //            return NotFound();
    //        }
    //        ViewData["TransactionCategoryID"] = new SelectList(_context.TransactionCategories, "ID", "Name", transaction.TransactionCategoryID);
    //        ViewData["PaymentTypeID"] = new SelectList(_context.PaymentTypes, "ID", "ID", transaction.PaymentTypeID);
    //        return View(transaction);
    //    }

    //    // POST: Transaction/Edit/5
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,Category,TransactionCategoryID,TransactionDate,CreateDate,ModyficationDate,Price,PaymentTypeID")] Transaction transaction)
    //    {
    //        if (id != transaction.ID)
    //        {
    //            return NotFound();
    //        }

    //        if (ModelState.IsValid)
    //        {
    //            try
    //            {
    //                _context.Update(transaction);
    //                await _context.SaveChangesAsync();
    //            }
    //            catch (DbUpdateConcurrencyException)
    //            {
    //                if (!TransactionExists(transaction.ID))
    //                {
    //                    return NotFound();
    //                }
    //                else
    //                {
    //                    throw;
    //                }
    //            }
    //            return RedirectToAction(nameof(Index));
    //        }
    //        ViewData["TransactionCategoryID"] = new SelectList(_context.TransactionCategories, "ID", "Name", transaction.TransactionCategoryID);
    //        ViewData["PaymentTypeID"] = new SelectList(_context.PaymentTypes, "ID", "ID", transaction.PaymentTypeID);
    //        return View(transaction);
    //    }

    //    // GET: Transaction/Delete/5
    //    public async Task<IActionResult> Delete(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var transaction = await _context.Transactions
    //            .Include(t => t.Category)
    //            .Include(t => t.PaymentType)
    //            .FirstOrDefaultAsync(m => m.ID == id);
    //        if (transaction == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(transaction);
    //    }

    //    // POST: Transaction/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> DeleteConfirmed(int id)
    //    {
    //        var transaction = await _context.Transactions.FindAsync(id);
    //        _context.Transactions.Remove(transaction);
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }

    //    private bool TransactionExists(int id)
    //    {
    //        return _context.Transactions.Any(e => e.ID == id);
    //    }
    //}
}
