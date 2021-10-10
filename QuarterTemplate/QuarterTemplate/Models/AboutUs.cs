using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Models
{
    public class AboutUs
    {
        public int Id { get; set; }
        [StringLength(maximumLength:200)]
        public string Title { get; set; }
        [StringLength(maximumLength: 200)]

        public string Image { get; set; }

        [StringLength(maximumLength: 200)]

        public string Description1 { get; set; }
        [StringLength(maximumLength: 200)]

        public string Icon { get; set; }
        [StringLength(maximumLength: 200)]

        public string IconName { get; set; }
        [StringLength(maximumLength: 200)]

        public string Description2 { get; set; }
        [StringLength(maximumLength: 200)]

        public string Image2 { get; set; }

    }
}
