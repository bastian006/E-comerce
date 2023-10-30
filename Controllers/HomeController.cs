using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using E_Commerce.Models;

namespace E_Commerce.Models
{
    public class HomeController : Controller
    {
        private MyContext _context;
        public HomeController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Dashboard()
        {
            Wrapper Wrapper = new Wrapper();
            Wrapper.AllCustomers = _context.Customers.OrderByDescending(c => c.CreatedAt).ToList();
            Wrapper.AllOrders = _context.Orders.Include(o => o.Customer).Include(o => o.Product).OrderByDescending(o => o.CreatedAt).ToList();
            Wrapper.AllProducts = _context.Products.OrderByDescending(p => p.CreatedAt).ToList();
            return View("Dashboard", Wrapper);
        }

        [HttpGet("orders")]
        public IActionResult Orders()
        {
            Wrapper Wrapper = new Wrapper();
            Wrapper.AllCustomers = _context.Customers.OrderBy(c => c.Name).ToList();
            Wrapper.AllOrders = _context.Orders.Include(o => o.Customer).Include(o => o.Product).OrderByDescending(o => o.CreatedAt).ToList();
            Wrapper.AllProducts = _context.Products.OrderByDescending(p => p.CreatedAt).ToList();
            return View("Orders", Wrapper);
        }

        [HttpGet("customers")]
        public IActionResult Customers()
        {
            Wrapper Wrapper = new Wrapper();
            Wrapper.AllCustomers = _context.Customers.OrderByDescending(c => c.CreatedAt).ToList();
            return View("Customers", Wrapper);
        }
        [HttpPost("customers")]
        public IActionResult CreateCustomer(Wrapper Form)
        {
            if (ModelState.IsValid)
            {
                if (_context.Customers.Any(c => c.Name == Form.Customer.Name))
                {
                    ModelState.AddModelError("Customer.Name", "Customer Name already added.");
                    return Customers();
                }
                _context.Add(Form.Customer);
                _context.SaveChanges();
                return RedirectToAction("Customers");
            }
            else
            {
                return Customers();
            }
        }
        [HttpPost("customers/delete/{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            Customer ToDelete = _context.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (ToDelete == null)
            {
                return RedirectToAction("Customers");
            }
            _context.Remove(ToDelete);
            _context.SaveChanges();
            return RedirectToAction("Customers");
        }

        [HttpGet("products")]
        public IActionResult Products()
        {
            Wrapper Wrapper = new Wrapper();
            Wrapper.AllProducts = _context.Products.OrderBy(p => p.Name).ToList();
            return View("Products", Wrapper);
        }
        [HttpPost("products")]
        public IActionResult CreateProduct(Wrapper Form)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Form.Product);
                _context.SaveChanges();
                return RedirectToAction("Products");
            }
            else
            {
                return Products();
            }
        }

        [HttpPost("orders")]
        public IActionResult CreateOrder(int Customer, Wrapper Form, int Product)
        {
            if (!ModelState.IsValid)
            {
                return Orders();
            }
            Product product = _context.Products.FirstOrDefault(p => p.ProductId == Product);
            if (product.Quantity < Form.Order.Quantity)
            {
                ModelState.AddModelError("Order.Quantity", "Not enough inventory. Please order less");
                return Orders();
            }
            product.Quantity -= Form.Order.Quantity;
            _context.Update(product);
            _context.SaveChanges();
            Form.Order.ProductId = Product;
            Form.Order.CustomerId = Customer;
            _context.Add(Form.Order);
            _context.SaveChanges();
            return RedirectToAction("Orders");
        }
    }
}