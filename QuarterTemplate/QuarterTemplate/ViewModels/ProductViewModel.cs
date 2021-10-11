using QuarterTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.ViewModels
{
    public class ProductViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Aminity> Aminities { get; set; }
        public List<Status> Statuses { get; set; }
        public List<Product> Products { get; set; }

    }
}
