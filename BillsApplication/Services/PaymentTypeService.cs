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
    }
}
