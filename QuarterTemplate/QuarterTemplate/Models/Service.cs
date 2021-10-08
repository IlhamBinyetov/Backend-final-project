using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Models
{
    public class Service
    {
        public int Id { get; set; }
        public int Order { get; set; }
      
        public string Icon { get; set; }
        [Required]
        [StringLength(maximumLength: 150)]

        public string Title { get; set; }
        [StringLength(maximumLength: 300)]

        public string Description { get; set; }
        [StringLength(maximumLength: 150)]

        public string UrlText { get; set; }
    }
}
