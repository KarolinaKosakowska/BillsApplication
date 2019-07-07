using BillsData;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillsApplication.Services
{
    public interface ICategory
    {
        DbSet<TransactionCategory> GetAll();
        void Add(TransactionCategory transactionCategory);
        SelectList GetTransactionCategories();
        void EditCategory(TransactionCategory transactionCategory);
        void DeleteCategory(int id);
    }
}
