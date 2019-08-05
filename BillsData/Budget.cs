using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace BillsData
{
    public class Budget:BaseEntity
    {
        [Required]

        public string Name { get; set; }
        [Display(Name = "Transaction category")]
        public int? TransactionCategoryId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public decimal Limit { get; set; }
        [Required]
        public DateTime From { get; set; }
        [Required]
        public DateTime To { get; set; }
        [Required]
        [Display(Name = "Create date")]
        public DateTime CreateDate { get; set; }
        [Required]
        [Display(Name = "Modification date")]
        public DateTime ModyficationDate { get; set; }
        public int UserId { get; set; }

        public virtual IdentityUser User { get; set; }
        public virtual TransactionCategory TransactionCategory { get; set; }
    }
}
