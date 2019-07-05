using BillsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillsData.Helpers
{
    public static class EntitiesFromEnum
    {
        public static IEnumerable<EnumEntity> BuildEntityObjectsFromEnum<U>() where U : Enum
        {
            var listObjectsToReturn = Enum.GetValues(typeof(U))
               .Cast<U>()
               .Select(t => new EnumEntity
               {
                   Id = Convert.ToInt32(t),
                   Name = t.ToString()
               });

            return listObjectsToReturn;
        }
    }
}

