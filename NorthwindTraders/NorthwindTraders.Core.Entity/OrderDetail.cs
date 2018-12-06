using System;
using System.Collections.Generic;
using System.Text;

namespace NorthwindTraders.Core.Entity
{
    public class OrderDetail
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
    }
}
