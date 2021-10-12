using QuarterTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.ViewModels
{
    public class DetailViewModel
    {
        public List<Category> Categories { get; set; }
        public Product Product { get; set; }
    }
}
