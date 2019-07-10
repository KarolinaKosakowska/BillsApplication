﻿
using BillsApplication.Data;
using BillsApplication.Models.TransactionForm;
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

        public TransactionService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(Transaction newTransaction)
        {
            context.Add(newTransaction);
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
       
        public string GetTransactionTag(int id)
        {
            if (context.Tags.Any(a => a.Id == id))
            {
                return context.Tags
                    .FirstOrDefault(a => a.Id == id).Name;
            }
            else return "";
        }


        public string GetProduct(int id)
        {
            if (context.Products.Any(a => a.Id == id))
            {
                return context.Products
                    .FirstOrDefault(a => a.Id == id).Name;
            }
            else return "";
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
            context.Update(transaction);
            context.SaveChangesAsync();
        }
        public void DeleteTransaction(int id)
        {
            var transaction= context.Transactions.Find(id);
            context.Transactions.Remove(transaction);
            context.SaveChangesAsync();
        }
        public DateTime SetCreationDate()
        {
            return DateTime.Now;
        }


    }
}
