using System;

namespace NorthwindTraders.Core.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public string ShipVia { get; set; }
        //public Freight" "money" NULL CONSTRAINT "DF_Orders_Freight" DEFAULT(0),
        //public ShipName" nvarchar(40) NULL ,
        //public ShipAddress" nvarchar(60) NULL ,
        //public ShipCity" nvarchar(15) NULL ,
        //public ShipRegion" nvarchar(15) NULL ,
        //public ShipPostalCode" nvarchar(10) NULL ,
        //public ShipCountry
        //public EmployeeID" "int" NULL ,
    }
}