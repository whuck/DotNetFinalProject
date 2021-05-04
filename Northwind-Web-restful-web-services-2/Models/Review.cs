using System.ComponentModel.DataAnnotations;

namespace Northwind.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public decimal Rating { get; set; }
        public System.DateTime ReviewDate { get; set; }
        public string Description { get; set; }

        public Customer Customer { get; set; }
        public Product Product { get; set; }
    }
}
