﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillsApplication.Services
{
    public interface IPaymentType
    {
        SelectList GetPaymentTypes();
        string GetPaymentType(int id);
    }
}
