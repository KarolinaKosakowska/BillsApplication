using BillsApplication.Data;
using BillsData;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillsApplication.Services.Budget
{
    public class BudgetService : IBudget
    {
        private readonly ApplicationDbContext context;

        public BudgetService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(BillsData.Budget budget)
        {
            context.Add(budget);
            context.SaveChanges();
        }

        public SelectList GetBudgets()
        {
            var budgets = new SelectList(context.Budgets, "Id", "Limit");
            return budgets;
        }
        public DbSet<BillsData.Budget> GetAll()
        {
            return context.Budgets;
        }
        public string GetBudget(int? id)
        {
            if (context.Budgets.Any(a => a.Id == id))
            {
                return context.Budgets.Include(asset => asset.TransactionCategory)
                    .FirstOrDefault(a => a.Id == id).Name;
            }
            else return "";
        }
        public decimal SetBudgetAmount()
        {         
            decimal budgetAmount = Convert.ToDecimal(from a in context.Transactions
                                                                from b in context.Budgets
                                                                where a.TransactionCategory.Name == b.TransactionCategory.Name 
                                                                select  b.Limit-a.Price
                                                               );
               return budgetAmount;
        }

        public void EditBudget(BillsData.Budget budget)
        {
            context.Update(budget);
            context.SaveChangesAsync();
        }
        public void DeleteBudget(int id)
        {
            var budget = context.Budgets.Find(id);
            context.Budgets.Remove(budget);
            context.SaveChangesAsync();
        }


    }
}
