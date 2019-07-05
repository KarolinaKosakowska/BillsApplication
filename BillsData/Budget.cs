using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace BillsData
{
    public class Budget:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public int? TransactionCategoryId { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public DateTime From { get; set; }
        [Required]
        public DateTime To { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime ModyficationDate { get; set; }
        public int UserId { get; set; }

        public virtual IdentityUser User { get; set; }
        public virtual TransactionCategory TransactionCategory { get; set; }
    }
}
