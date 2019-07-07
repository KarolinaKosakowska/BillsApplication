using BillsApplication.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public SelectList GetTransactionCategories()
        {
            var transactionCategories = new SelectList(context.TransactionCategories, "Id", "Name");
            return transactionCategories;
        }
    }
}
