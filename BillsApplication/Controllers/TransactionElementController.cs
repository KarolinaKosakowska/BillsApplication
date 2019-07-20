using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BillsApplication.Data;
using BillsApplication.Services.TransactionElement;
using BillsData;
using BillsApplication.Services;

namespace BillsApplication.Controllers
{
    public class TransactionElementController : Controller
    {
        private readonly ITransactionElement _transactionElementService;
        private readonly IProduct _productService;
        private readonly ITransaction _transactionService;
        private readonly IUnit _unitService;

        public TransactionElementController(ITransactionElement transactionElementService, IProduct productService, ITransaction transactionService, IUnit unitService)
        {
            _transactionElementService = transactionElementService;
            _productService = productService;
            _transactionService = transactionService;
            _unitService = unitService;
        }

        // GET: TransactionElement
        public IActionResult Index()
        {
            return View(_transactionElementService.GetAll());
        }

        // GET: TransactionElement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionElement = await _transactionElementService.GetAll()
                .Include(t => t.Product)
                .Include(t => t.Transaction)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transactionElement == null)
            {
                return NotFound();
            }

            return View(transactionElement);
        }

        // GET: TransactionElement/Create
        public IActionResult Create()
        {
            
            ViewData["ProductId"] = _productService.GetProducts();
            ViewData["TransactionId"] = _transactionService.GetTransactions();
            return View();
        }

        // POST: TransactionElement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TransactionId,ProductId,Price,Amount,Id")] TransactionElement transactionElement)
        {
            if (ModelState.IsValid)
            {
                _transactionElementService.Add(transactionElement);
                return RedirectToAction(nameof(Index));
            }
            ViewData["TransactionId"] = _transactionService.GetTransactions();
            ViewData["ProductId"] = _productService.GetProducts();
            return View(transactionElement);
        }

        // GET: TransactionElement/Edit/5
        //    public async Task<IActionResult> Edit(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var transactionElement = await _context.TransactionElements.FindAsync(id);
        //        if (transactionElement == null)
        //        {
        //            return NotFound();
        //        }
        //        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", transactionElement.ProductId);
        //        ViewData["TransactionId"] = new SelectList(_context.Transactions, "Id", "Id", transactionElement.TransactionId);
        //        return View(transactionElement);
        //    }

        //    // POST: TransactionElement/Edit/5
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(int id, [Bind("TransactionId,ProductId,Price,Amount,Id")] TransactionElement transactionElement)
        //    {
        //        if (id != transactionElement.Id)
        //        {
        //            return NotFound();
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                _context.Update(transactionElement);
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!TransactionElementExists(transactionElement.Id))
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
        //        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", transactionElement.ProductId);
        //        ViewData["TransactionId"] = new SelectList(_context.Transactions, "Id", "Id", transactionElement.TransactionId);
        //        return View(transactionElement);
        //    }

        //    // GET: TransactionElement/Delete/5
        //    public async Task<IActionResult> Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var transactionElement = await _context.TransactionElements
        //            .Include(t => t.Product)
        //            .Include(t => t.Transaction)
        //            .FirstOrDefaultAsync(m => m.Id == id);
        //        if (transactionElement == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(transactionElement);
        //    }

        //    // POST: TransactionElement/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(int id)
        //    {
        //        var transactionElement = await _context.TransactionElements.FindAsync(id);
        //        _context.TransactionElements.Remove(transactionElement);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    private bool TransactionElementExists(int id)
        //    {
        //        return _context.TransactionElements.Any(e => e.Id == id);
        //    }
        //}\
    }
}
