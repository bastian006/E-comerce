using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Name: ")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Image (URL): ")]
        public string Image { get; set; }

        [Required]
        [Display(Name = "Description: ")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Quantity: ")]
        public int Quantity { get; set; }

        public List<Order> Orders { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}