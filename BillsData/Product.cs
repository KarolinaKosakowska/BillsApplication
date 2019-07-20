using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillsData
{
    public class Product: BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Unit { get; set; }
        public virtual ICollection<TransactionElement> TransactionElements { get; set; }
    }
    
}
