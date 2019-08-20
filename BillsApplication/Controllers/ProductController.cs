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
using BillsApplication.Services.TransactionElement;
using BillsApplication.Models.TransactionForm;
using Microsoft.AspNetCore.Authorization;

namespace BillsApplication.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IUnit _unitService;
        private readonly IProduct _productService;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductController(IUnit unitService, IProduct productService)
        {
            _unitService = unitService;
            _productService = productService;
        }

        // GET: Products

        public IActionResult Index()
        {
            var model = _productService.GetAll(); 
            return View(model);

        }

        //// GET: Products/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Products
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["Unit"] = _unitService.GetUnits();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product, TransactionElement transactionElement)
        {
            if (ModelState.IsValid)
            {
                _productService.Add(product);
                _productService.GetAll();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Unit"] = _unitService.GetUnits();

            return View(product);
        }
    }
}

//// GET: Products/Edit/5
//public async Task<IActionResult> Edit(int? id)
//{
//    if (id == null)
//    {
//        return NotFound();
//    }

//    var product = await _context.Products.FindAsync(id);
//    if (product == null)
//    {
//        return NotFound();
//    }
//    return View(product);
//}

//// POST: Products/Edit/5
//// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//[HttpPost]
//[ValidateAntiForgeryToken]
//public async Task<IActionResult> Edit(int id, [Bind("Name,Unit,Id")] Product product)
//{
//    if (id != product.Id)
//    {
//        return NotFound();
//    }

//    if (ModelState.IsValid)
//    {
//        try
//        {
//            _context.Update(product);
//            await _context.SaveChangesAsync();
//        }
//        catch (DbUpdateConcurrencyException)
//        {
//            if (!ProductExists(product.Id))
//            {
//                return NotFound();
//            }
//            else
//            {
//                throw;
//            }
//        }
//        return RedirectToAction(nameof(Index));
//    }
//    return View(product);
//}

//// GET: Products/Delete/5
//public async Task<IActionResult> Delete(int? id)
//{
//    if (id == null)
//    {
//        return NotFound();
//    }

//    var product = await _context.Products
//        .FirstOrDefaultAsync(m => m.Id == id);
//    if (product == null)
//    {
//        return NotFound();
//    }

//    return View(product);
//}

//// POST: Products/Delete/5
//[HttpPost, ActionName("Delete")]
//[ValidateAntiForgeryToken]
//public async Task<IActionResult> DeleteConfirmed(int id)
//{
//    var product = await _context.Products.FindAsync(id);
//    _context.Products.Remove(product);
//    await _context.SaveChangesAsync();
//    return RedirectToAction(nameof(Index));
//}

//private bool ProductExists(int id)
//{
//    return _context.Products.Any(e => e.Id == id);
//}


