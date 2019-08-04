
using BillsApplication.Data;
using BillsApplication.Models.TransactionForm;
using BillsApplication.Services.Budget;
using BillsData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillsApplication
{
    public class TransactionService : ITransaction
    {
        private readonly ApplicationDbContext context;
        private readonly IBudget budgetSevice;

        public TransactionService(ApplicationDbContext context, IBudget budgetSevice )
        {
            this.context = context;
            this.budgetSevice = budgetSevice;
        }

        public void Add(Transaction newTransaction,Budget newBudget)
        {
            newTransaction.CreateDate = DateTime.Now;
            newBudget.Amount = budgetSevice.SetBudgetAmount();
            context.Add(newTransaction);
            context.Add(newBudget);
            context.SaveChanges();
        }

        public IEnumerable<Transaction> GetAll()
        {
            return context.Transactions
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
