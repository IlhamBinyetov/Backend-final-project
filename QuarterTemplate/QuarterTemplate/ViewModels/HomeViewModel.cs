using QuarterTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.ViewModels
{
    public class HomeViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List<Service> Services { get; set; }
        public AboutUs AboutUses { get; set; }
        public List<About> Abouts { get; set; }
        public List<City> Cities { get; set; }
        public List<Status> Statuses { get; set; }
        public List<Category> Categories { get; set; }

      
    }
}
