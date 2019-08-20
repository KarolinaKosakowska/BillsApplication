using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillsData
{
    public class Transaction: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int? BudgetId { get; set; }
        public int? TransactionCategoryId { get; set; }
        [Required]
        [Display(Name = "Transaction date")]
        public DateTime TransactionDate { get; set; }
        [Required]
        [Display(Name = "Create date")]
        public DateTime CreateDate { get; set; }
        [Required]
        [Display(Name = "Modyfication date")]
        public DateTime ModyficationDate { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int? PaymentTypeId { get; set; }
        [Display(Name = "Transaction category")]
        public virtual TransactionCategory TransactionCategory { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        [Display(Name = "Payment type")]
        public virtual PaymentType PaymentType { get; set; }
        [Display(Name = "Budget name")]
        public virtual Budget Budget { get; set; }
        public virtual ICollection<TransactionTag> TransactionTags { get; set; }
        public virtual ICollection<TransactionElement> TransactionElements { get; set; }
        [Display(Name = "Attachment/bill")]
        public virtual ICollection<File> Files { get; set; }
    }
}
