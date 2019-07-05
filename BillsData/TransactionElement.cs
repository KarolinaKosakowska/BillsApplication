using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillsData
{
    public class TransactionElement: BaseEntity
    {
        [Required]
        public int TransactionId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Amount { get; set; }

        public virtual Transaction Transaction { get; set; }
        public virtual Product Product { get; set; }

    }
}
