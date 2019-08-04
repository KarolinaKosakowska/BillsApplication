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
using BillsApplication.Services.Budget;

namespace BillsApplication.Controllers
{
    public class TransactionController : Controller
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransaction _transactionService;
        private readonly ICategory _categoryService;
        private readonly IPaymentType _paymentTypeService;
        private readonly IBudget _budgetService;
        private readonly ApplicationDbContext context;

        public TransactionController(ITransaction transactionService, ICategory categoryService,
            IPaymentType paymentTypeService, IBudget budgetService, ApplicationDbContext context)
        {
            _transactionService = transactionService;
            _categoryService = categoryService;
            _paymentTypeService = paymentTypeService;
            _budgetService = budgetService;
            this.context = context;
        }

        // GET: Transaction
        public IActionResult Index()
        {
            var assetModels = _transactionService.GetAll();
            var resultTransaction = assetModels
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
            var resultBudget = _budgetService.GetAll().Include(b => b.TransactionCategory).ToList();
            var model = new TransactionIndexModel { TransactionsListingModels = resultTransaction, Budget = resultBudget };
           
            return View(model);
        }


        // GET: Transaction/Details/5
        public IActionResult Details(int id)
        {
            var asset = _transactionService.GetById(id);
            var model = new DetailsModel
            {
                TransactionCategory = asset.TransactionCategory.Name,
                Name = asset.Name,
                Id = id,
                Description = asset.Description,
                TransactionDate = asset.TransactionDate,
                Price = asset.Price,
                PaymentType = asset.PaymentType.Name,
                CreationDate = asset.CreateDate,
                ModyficationDate = asset.ModyficationDate,
                TransactionTags = _transactionService.GetTag(asset.Id).ToList(),
                Product = _transactionService.GetProduct(asset.Id).ToList()
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
        public IActionResult Create(Transaction transaction, Budget budget)
        {
            if (ModelState.IsValid)
            {
                // transaction.UserID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;              
                _transactionService.Add(transaction,budget);

                return RedirectToAction(nameof(Index));
            }
            ViewData["TransactionId"] = _transactionService.GetTransactions();
            ViewData["PaymentTypeId"] = _paymentTypeService.GetPaymentTypes();
            ViewData["TransactionCategoryId"] = _categoryService.GetTransactionCategories();
            return View(transaction);

        }

        // GET: Transaction/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = _transactionService.GetById(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["PaymentTypeId"] = _paymentTypeService.GetPaymentTypes();
            ViewData["TransactionCategoryId"] = _categoryService.GetTransactionCategories();
            return View(transaction);
        }

        // POST: Transaction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Description,Category,TransactionCategoryId,TransactionDate,CreateDate,ModyficationDate,Price,PaymentTypeId")] Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _transactionService.EditTransaction(transaction);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Id))
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
            ViewData["PaymentTypeId"] = _paymentTypeService.GetPaymentTypes();
            ViewData["TransactionCategoryId"] = _categoryService.GetTransactionCategories();
            return View(transaction);
        }

        // GET: Transaction/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = _transactionService.GetById(id);

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
