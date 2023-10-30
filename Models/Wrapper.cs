using System.Collections.Generic;

namespace E_Commerce.Models
{
    public class Wrapper
    {
        public Customer Customer { get; set; }
        public List<Customer> AllCustomers { get; set; }
        public Product Product { get; set; }
        public List<Product> AllProducts { get; set; }
        public Order Order { get; set; }
        public List<Order> AllOrders { get; set; }
    }
}