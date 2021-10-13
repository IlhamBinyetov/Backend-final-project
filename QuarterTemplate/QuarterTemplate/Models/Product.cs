using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Models
{
    public class Product
    {
        public int Id { get; set; }
        [StringLength(maximumLength:100)]
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }
        [StringLength(maximumLength:100)]
        public string Location { get; set; }
        [StringLength(maximumLength: 2000)]

        
        public string Description { get; set; }
        [StringLength(maximumLength: 100)]
        public decimal HomeArea { get; set; }
        [StringLength(maximumLength:100)]
        public string BuiltDate { get; set; }
        public int BedCount { get; set; }
        [StringLength(maximumLength: 100)]
        public int RoomCount { get; set; }
        [StringLength(maximumLength: 100)]
        public int BathCount { get; set; }
        [StringLength(maximumLength: 100)]
        public decimal Price { get; set; }
        public bool IsFeature { get; set; } 
        public List<ProductAminity> productAminities { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public Status Status { get; set; }
        public int StatusId { get; set; }
        public City City { get; set; }
        public int CityId { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public Team Team { get; set; }
        public int TeamId { get; set; }


        [NotMapped]
        public IFormFile PosterFile { get; set; }

        [NotMapped]
        public List<IFormFile> ImageFiles { get; set; }



    }
}
