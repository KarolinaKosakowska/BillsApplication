using BillsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillsApplication.Models.TransactionForm
{
    public class ProductCreateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
  
        public string Unit { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
