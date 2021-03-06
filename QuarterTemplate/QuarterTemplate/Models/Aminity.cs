using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Models
{
    public class Aminity
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 100)]

       public string Icon { get; set; }
        [StringLength(maximumLength: 50)]

        public string Title { get; set; }

        public List<ProductAminity> productAminities { get; set; }
    }
}
