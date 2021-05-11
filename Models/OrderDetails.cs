using System;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Models
{
    public class OrderDetails
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public Decimal Discount { get; set; }
        
        
        public Product product { get; set; }
        public Order order {get; set;}

        
    }
}
