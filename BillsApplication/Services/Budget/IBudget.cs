using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillsApplication.Services.Budget
{
    public interface IBudget
    {
        void Add(BillsData.Budget budget);
        decimal SetBudgetAmount(BillsData.Budget budget);
        DbSet<BillsData.Budget> GetAll();
        string GetBudget(int? id);
        void EditBudget(BillsData.Budget budget);
        void DeleteBudget(int id);

    }
}
