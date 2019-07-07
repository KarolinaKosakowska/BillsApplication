using BillsApplication.Models.TransactionForm;
using BillsData;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BillsApplication
{
    public interface ITransaction
    {
        IEnumerable<BillsData.Transaction> GetAll();
        BillsData.Transaction GetById(int id);
        string GetCategory(int id);
        string GetPaymentType(int id);
        string GetTransactionTag(int id);
        string GetProduct(int id);
        int GetAmout(int id);
        string GetUnit(int id);
        SelectList GetTransactionCategories();
       // string GetAttachment(int id);

        void Add(CreateModel newTransaction);
        //Task<List<Transaction>> GetList(int page = 1, int? pageLocalSize = null);
        //int TotalItems { get; set; }
    }
}
