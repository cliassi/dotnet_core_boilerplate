using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class NameDto
    {
        public String Name { get; set; }
    }
    public class SimpleProductObject
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public Nullable<int> PurchasingUnit { get; set; }
        public Nullable<int> IssuingUnit { get; set; }
        public Nullable<bool> MultiUnit { get; set; }
        public String Units { get; set; }
        public bool HasVariation { get; set; }
    }
    public class ProductInventoryDetailsDto
    {
        public int Product { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Nullable<int> Variation { get; set; }
        public Nullable<int> Unit { get; set; }
    }

    public class StorePeriod
    {
        public int StoreId { get; set; }
        public DateTime EndDate { get; set; }
    }
}
