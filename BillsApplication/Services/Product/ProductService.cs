using BillsApplication.Data;
using BillsData;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillsApplication.Services
{
    public class ProductService: IProduct
    {
        private readonly ApplicationDbContext context;

        public ProductService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(Product product)
        {
            context.Add(product);
            context.SaveChanges();
        }
        public DbSet<Product> GetAll()
        {
            return context.Products;
        }

        public string GetProduct(int id)
        {
            if (context.Products.Any(a => a.Id == id))
            {
                return context.Products
                    .FirstOrDefault(a => a.Id == id).Name;
            }
            else return "";
        }
        public SelectList GetProducts()
        {
            var products= new SelectList(context.Products, "Id", "Name");
            return products;
        }

        //public void EditPro(TransactionCategory transactionCategory)
        //{
        //    context.Update(transactionCategory);
        //    context.SaveChangesAsync();
        //}
        //public void DeleteCategory(int id)
        //{
        //    var transactionCategory = context.TransactionCategories.Find(id);
        //    context.TransactionCategories.Remove(transactionCategory);
        //    context.SaveChangesAsync();
        //}
    }
}

