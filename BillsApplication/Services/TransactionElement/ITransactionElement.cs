using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillsApplication.Services.TransactionElement
{
    public interface ITransactionElement
    {
        void Add(BillsData.TransactionElement transactionElement);

        DbSet<BillsData.TransactionElement> GetAll();
    }
}
