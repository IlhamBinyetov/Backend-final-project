using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public bool IsPoster { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
