using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Models
{
    public class Team
    {
        public int Id { get; set; }
        public int Order { get; set; }
        [StringLength(maximumLength: 100)]
        public string Image { get; set; }
        [Required]
        [StringLength(maximumLength:50)]
        public string Name { get; set; }
        [StringLength(maximumLength: 150)]

        public string FacebookUrl { get; set; }
        [StringLength(maximumLength: 150)]

        public string TwitterUrl { get; set; }
        [StringLength(maximumLength: 150)]

        public string LinkedinUrl { get; set; }
        public List<Product> Products { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
