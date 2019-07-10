using BillsApplication.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillsApplication.Services
{
    public class PaymentTypeService: IPaymentType
    {
        private readonly ApplicationDbContext context;

        public PaymentTypeService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public SelectList GetPaymentTypes()
        {
            var paymentTypes = new SelectList(context.PaymentTypes, "Id", "Name");
            return paymentTypes;
        }
        public string GetPaymentType(int id)
        {
            if (context.Transactions.Any(a => a.Id == id))
            {
                return context.Transactions
                    .FirstOrDefault(a => a.Id == id).PaymentType.Name.ToString();
            }
            else return "";
        }
    }
}
