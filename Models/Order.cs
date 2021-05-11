namespace Northwind.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId {get;set;}
        public System.DateTime OrderDate {get;set;}
        public Customer customer {get;set;}
    }
}