using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Northwind.Models
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>()
            .HasKey(od => new { od.OrderId, od.ProductId });
        }

        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
            SaveChanges();
        }
        public void EditCustomer(Customer customer)
        {
            var customerToUpdate = Customers.FirstOrDefault(c => c.CustomerID == customer.CustomerID);
            customerToUpdate.Address = customer.Address;
            customerToUpdate.City = customer.City;
            customerToUpdate.Region = customer.Region;
            customerToUpdate.PostalCode = customer.PostalCode;
            customerToUpdate.Country = customer.Country;
            customerToUpdate.Phone = customer.Phone;
            customerToUpdate.Fax = customer.Fax;
            SaveChanges();
        }
        public CartItem AddToCart(CartItemJSON cartItemJSON)
        {
            int CustomerId = Customers.FirstOrDefault(c => c.Email == cartItemJSON.email).CustomerID;
            int ProductId = cartItemJSON.id;
            // check for duplicate cart item
            CartItem cartItem = CartItems.FirstOrDefault(ci => ci.ProductId == ProductId && ci.CustomerId == CustomerId);
            if (cartItem == null)
            {
                // this is a new cart item
                cartItem = new CartItem()
                {
                    CustomerId = CustomerId,
                    ProductId = cartItemJSON.id,
                    Quantity = cartItemJSON.qty
                };
                CartItems.Add(cartItem);
            }
            else
            {
                // for duplicate cart item, simply update the quantity
                cartItem.Quantity += cartItemJSON.qty;
            }

            SaveChanges();
            cartItem.Product = Products.Find(cartItem.ProductId);
            return cartItem;
        }

        public Review AddReview(ReviewJSON reviewJSON)
        {
            int CustomerId = Customers.FirstOrDefault(c => c.Email == reviewJSON.email).CustomerID;
            int ProductId = reviewJSON.id;
            // check for duplicate cart item
            Review review = Reviews.FirstOrDefault(r => r.ProductId == ProductId && r.CustomerId == CustomerId);
            var Order = Orders.Join(OrderDetails, o => o.OrderId, od => od.OrderId, (o, od) => new { orderID = o.OrderId, productId = od.ProductId, customerId = o.CustomerId }).FirstOrDefault(i => i.productId == ProductId && i.customerId == CustomerId);

            if (Order != null)
            {
                if (review == null)
                {
                    // this is a new cart item
                    review = new Review()
                    {
                        CustomerId = CustomerId,
                        ProductId = reviewJSON.id,
                        Rating = reviewJSON.rating,
                        Description = reviewJSON.description,
                        ReviewDate = System.DateTime.Now
                    };
                    Reviews.Add(review);
                } else {
                    return review;
                }
            } else {
            }

            SaveChanges();
            review.Product = Products.Find(review.ProductId);
            return review;
        }
    }
}
