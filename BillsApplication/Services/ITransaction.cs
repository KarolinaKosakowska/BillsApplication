﻿using BillsApplication.Models.TransactionForm;
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
        string GetProduct(int id);
        int GetAmout(int id);
        string GetUnit(int id);
        void DeleteTransaction(int id);
        void EditTransaction(BillsData.Transaction transaction);
        SelectList GetTransactions();

        void Add(BillsData.Transaction newTransaction);
        //Task<List<Transaction>> GetList(int page = 1, int? pageLocalSize = null);
        //int TotalItems { get; set; }
    }
}
