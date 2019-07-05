using System;
using System.Collections.Generic;
using System.Text;

namespace BillsData
{
    public class TransactionTag: BaseEntity
    {
        public int TransactionId { get; set; }
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}
