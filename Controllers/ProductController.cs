using Microsoft.AspNetCore.Mvc;
using Northwind.Models;
using System.Linq;

namespace Northwind.Controllers
{
    public class ProductController : Controller
    {
        // this controller depends on the NorthwindRepository
        private NorthwindContext _northwindContext;
        public ProductController(NorthwindContext db) => _northwindContext = db;
        public IActionResult Category() => View(_northwindContext.Categories.OrderBy(c => c.CategoryName));
        public IActionResult Index(int id){
            ViewBag.id = id;
            return View(_northwindContext.Categories.OrderBy(c => c.CategoryName));
        }

        public IActionResult SingleProduct(int productId){
            ViewBag.productId = productId;
            return View(_northwindContext.Products.Where(p => p.ProductId == productId));
        }
        // public IActionResult SingleProduct(int id) => View(_northwindContext.Products.Where(p => p.ProductId == id));

    }
}
