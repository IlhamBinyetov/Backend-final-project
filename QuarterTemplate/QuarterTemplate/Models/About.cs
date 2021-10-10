using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Models
{
    public class About
    {
        public int Id { get; set; }
        
        public int Order { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
        public int AboutUsId { get; set; }
        public AboutUs AboutUs { get; set; } 
    }
}
