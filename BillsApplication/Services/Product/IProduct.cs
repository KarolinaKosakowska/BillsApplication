using BillsData;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillsApplication.Services
{
    public interface IProduct
    {
        void Add(Product product);

        DbSet<Product> GetAll();
        string GetProduct(int id);
        SelectList GetProducts();
    }
}
