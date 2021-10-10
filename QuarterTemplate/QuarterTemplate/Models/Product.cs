using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public decimal HomeArea { get; set; }
        public string BuiltDate { get; set; }
        public int BedCount { get; set; }
        public int RoomCount { get; set; }
         public int BathCount { get; set; }
        public decimal Price { get; set; }
        public List<ProductAminity> productAminities { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public Status Status { get; set; }
        public int StatusId { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }
       

    }
}
