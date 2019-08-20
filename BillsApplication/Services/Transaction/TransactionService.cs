
using BillsApplication.Data;
using BillsApplication.Models.TransactionForm;
using BillsApplication.Services.Budget;
using BillsData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BillsApplication
{
    public class TransactionService : ITransaction
    {
        private readonly ApplicationDbContext context;
        private readonly IBudget budgetSevice;
        private readonly IHttpContextAccessor httpContextAccessor;

        public TransactionService(ApplicationDbContext context, IBudget budgetSevice, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.budgetSevice = budgetSevice;
            this.httpContextAccessor = httpContextAccessor;
        }
        private string GetCurrentUserId()
        {
            return httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public void Add(Transaction newTransaction)
        {
            newTransaction.CreateDate = DateTime.Today;
            newTransaction.UserId = GetCurrentUserId();
            context.Add(newTransaction);
            context.SaveChanges();
        }

        public IEnumerable<Transaction> GetAll()
        {          
            return context.Transactions.Where(t => t.UserId == GetCurrentUserId())
                .Include(asset => asset.TransactionCategory)
                .Include(asset => asset.PaymentType);
        }

        public Transaction GetById(int? id)
        {
            return
                GetAll()
                .FirstOrDefault(asset => asset.Id == id);
        }
        public SelectList GetTransactions()
        {
            var transactions = new SelectList(context.Transactions, "Id", "Name");
            return transactions;
        }

        public List<char> GetProduct(int id)
        {
            if (context.Transactions.Any(a => a.Id == id))
            {
                return context.TransactionElements
                    .SelectMany(x => x.Product.Name).ToList();
            }
            else return null;
        }
        public List<char> GetTag(int id)
        {
            if (context.Transactions.Any(a => a.Id == id))
            {
                return context.TransactionTags
                    .SelectMany(x => x.Tag.Name).ToList();
            }
            else return null;
        }
        public int GetAmout(int id)
        {
            if (context.TransactionElements.Any(a => a.Id == id))
            {
                return context.TransactionElements
                    .FirstOrDefault(a => a.Id == id).Amount;
            }
            else return 0;
        }
        public string GetUnit(int id)
        {
            if (context.Products.Any(a => a.Id == id))
            {
                return context.Products
                    .FirstOrDefault(a => a.Id == id).Unit.ToString();
            }
            else return "";
        }
        public void EditTransaction(Transaction transaction)
        {
            transaction.ModyficationDate = DateTime.Now;
            context.Update(transaction);
            context.SaveChangesAsync();
        }
        public void DeleteTransaction(int id)
        {
            var transaction= context.Transactions.Find(id);
            context.Transactions.Remove(transaction);
            context.SaveChangesAsync();
        }
    }
}
