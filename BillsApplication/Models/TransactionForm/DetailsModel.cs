using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BillsApplication.Models.TransactionForm
{
    public class DetailsModel
    {
        public int Id { get; set; }
        [Display(Name = "Category")]
        public string TransactionCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Transaction date")]
        public DateTime TransactionDate { get; set; }
        public decimal Price { get; set; }
        [Display(Name = "Payment type")]
        public string PaymentType { get; set; }
        [Display(Name = "Creation date")]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Modyfication date")]
        public DateTime ModyficationDate { get; set; }
        [Display(Name = "Tags")]
        public string TransactionTags { get; set; }
        public string Product { get; set; }
        public int Amount { get; set; }
        public int Unit { get; set; }
        public string Attachment { get; set; }


    }
}
