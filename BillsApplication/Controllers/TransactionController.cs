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
        private readonly IFile _fileService;

        public TransactionController(ITransaction transactionService, ICategory categoryService, IPaymentType paymentTypeService, IFile fileService)
        {
            _transactionService = transactionService;
            _categoryService = categoryService;
            _paymentTypeService = paymentTypeService;
            _fileService = fileService;
        }

        // GET: Transaction
        public IActionResult Index()
        {
            var assetModels = _transactionService.GetAll();
            var listingResult = assetModels
                .Select(result => new TransactionListingModel
                {
                    Id = result.Id,
                    TransactionCategory = _categoryService.GetCategory(result.Id),
                    Name = result.Name,
                    Description = result.Description,
                    TransactionDate = result.TransactionDate,
                    Price = result.Price,
                    PaymentType = _paymentTypeService.GetPaymentType(result.Id)

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
                TransactionCategory = _categoryService.GetCategory(id),
                Name = asset.Name,
                Description = asset.Description,
                TransactionDate = asset.TransactionDate,
                Price = asset.Price,
                PaymentType = _paymentTypeService.GetPaymentType(id),
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
            ViewData["PaymentTypeId"] = _paymentTypeService.GetPaymentTypes();
            ViewData["TransactionCategoryId"] = _categoryService.GetTransactionCategories();
            return View();
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Transaction transaction, File file, TransactionCategory transactionCategory)
        {
            if (ModelState.IsValid)
            {
                // transaction.UserID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                _transactionService.Add(transaction);
                _fileService.Add(file);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentTypeId"] = _paymentTypeService.GetPaymentTypes();
            ViewData["TransactionCategoryId"] = _categoryService.GetTransactionCategories();
            return View(transaction);

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

        // GET: Transaction/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction =  _transactionService.GetById(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _transactionService.DeleteTransaction(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return _transactionService.GetAll().Any(e => e.Id == id);
        }
    }
}
