using BillsApplication.Data;
using BillsData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillsApplication.Services.TransactionElement
{
    public class TransactionElementService: ITransactionElement
    {
        private readonly ApplicationDbContext context;

        public TransactionElementService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(BillsData.TransactionElement transactionElement)
        {
            context.Add(transactionElement);
            context.SaveChanges();
        }
        public DbSet<BillsData.TransactionElement> GetAll()
        {
            return context.TransactionElements;
        }
     
    }
}
