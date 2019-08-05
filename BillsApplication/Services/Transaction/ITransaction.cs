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
        BillsData.Transaction GetById(int? id);
        int GetAmout(int id);
        string GetUnit(int id);
        void DeleteTransaction(int id);
        void EditTransaction(BillsData.Transaction transaction);
        List<char> GetProduct(int id);
        List<char> GetTag(int id);
        SelectList GetTransactions();

        void Add(BillsData.Transaction newTransaction,Budget budget);
        //Task<List<Transaction>> GetList(int page = 1, int? pageLocalSize = null);
        //int TotalItems { get; set; }
    }
}
