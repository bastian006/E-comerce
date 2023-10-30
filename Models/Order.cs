using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        [Display(Name = "Quantity: ")]
        [Range(1, 10000000, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}