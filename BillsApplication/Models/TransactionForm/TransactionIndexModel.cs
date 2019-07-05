using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillsApplication.Models.TransactionForm
{
    public class TransactionIndexModel
    {
        public IEnumerable<TransactionListingModel>TransactionsListingModels { get; set; }
    }
}
