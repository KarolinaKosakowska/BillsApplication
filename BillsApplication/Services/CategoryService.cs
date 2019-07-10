using BillsApplication.Data;
using BillsData;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillsApplication.Services
{
    public class CategoryService: ICategory
    {
        private readonly ApplicationDbContext context;

        public CategoryService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Add(TransactionCategory transactionCategory)
        {
            context.Add(transactionCategory);
            context.SaveChanges();
        }
        public SelectList GetTransactionCategories()
        {
            var transactionCategories = new SelectList(context.TransactionCategories, "Id", "Name");
            return transactionCategories;
        }
        public DbSet<TransactionCategory> GetAll()
        {
            return context.TransactionCategories;
        }
        public string GetCategory(int id)
        {
            if (context.Transactions.Any(a => a.Id == id))
            {
                return context.Transactions
                    .FirstOrDefault(a => a.Id == id).TransactionCategory.Name;
            }
            else return "";
        }

        public void EditCategory(TransactionCategory transactionCategory)
        {
            context.Update(transactionCategory);
            context.SaveChangesAsync();
        }
        public void DeleteCategory(int id)
        {
            var transactionCategory = context.TransactionCategories.Find(id);
            context.TransactionCategories.Remove(transactionCategory);
            context.SaveChangesAsync();
        }
    }
}
