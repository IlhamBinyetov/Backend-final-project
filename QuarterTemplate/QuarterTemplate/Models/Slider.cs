using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required]
        public int Order { get; set; }

        [StringLength(maximumLength: 100)]
        public string Icon { get; set; }
        [StringLength(maximumLength:50)]
        public string SubTitle { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Title1 { get; set; }
        [StringLength(maximumLength: 50)]
        public string Title2 { get; set; }
        [StringLength(maximumLength: 200)]
        public string Description { get; set; }
        [StringLength(maximumLength: 200)]
        public string RedirectUrl { get; set; }
        [StringLength(maximumLength: 50)]
        public string RedirectUrlText { get; set; }
        [StringLength(maximumLength: 200)]
        public string Image { get; set; }
    }
}
