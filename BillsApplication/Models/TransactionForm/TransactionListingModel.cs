using BillsData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BillsApplication.Models.TransactionForm
{
    public class TransactionListingModel
    {
        public int Id { get; set; }
        public string TransactionCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Price { get; set; }
        public string PaymentType { get; set; }
    }

}


