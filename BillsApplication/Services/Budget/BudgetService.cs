using BillsApplication.Data;
using BillsApplication.Models.TransactionForm;
using BillsData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BillsApplication.Services.Budget
{
    public class BudgetService : IBudget
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public BudgetService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }
        private string GetCurrentUserId()
        {
            return httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public void Add(BillsData.Budget budget)
        {

            budget.CreateDate = DateTime.Now;
            budget.ModyficationDate = DateTime.Now;
            budget.UserId = GetCurrentUserId();
            context.Add(budget);
            context.SaveChanges();
        }

        public SelectList GetBudgets()
        {
            var budgets = new SelectList(context.Budgets, "Id", "Name");
            return budgets;
        }

        public IQueryable<BillsData.Budget> GetAll()
        {
            return context.Budgets.Where(t => t.UserId == GetCurrentUserId());
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
        public IQueryable<BudgetInTransactionList> SetBudgetAmount()
        {

            return context.Transactions.Join(context.Budgets,
                 t => t.BudgetId,
                 b => b.Id,
                 (t, b) =>
                 new BudgetInTransactionList
                 {
                     Id = b.Id,
                     Name = b.Name,
                     Limit = b.Limit,
                     Amount = b.Limit - t.Price,
                     UserId = b.UserId
                 }).Where(t => t.UserId == GetCurrentUserId());

        }

        public void EditBudget(BillsData.Budget budget)
        {
            budget.ModyficationDate = DateTime.Now;
            context.Update(budget);
            // var createDate = context.Budgets.FirstOrDefault(a => a.Id == id).CreateDate;     
            // budget.CreateDate = createDate;
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
