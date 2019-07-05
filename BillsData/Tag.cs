using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillsData
{
    public class Tag: BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public virtual ICollection<TransactionTag> TransactionTags { get; set; }
    }
}
