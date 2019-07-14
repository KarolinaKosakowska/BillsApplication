using BillsApplication.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillsApplication.Services
{
    public class UnitService: IUnit
    {
        private readonly ApplicationDbContext context;

        public UnitService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public SelectList GetUnits()
        {
            var units = new SelectList(context.Units, "Id", "Name");
            return units;
        }
    }
}
