using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillsApplication.Models.TransactionForm
{
    public class BudgetInTransactionList
    {
        public int Id { get; set; }
        public string Name { get; set; }       
        public decimal Amount { get; set; }
        public decimal Limit { get; set; }
        public string UserId { get; set; }
    }
}
