using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.ViewModels
{
    public class FilterViewModel
    {
        public int? CityId { get; set; }
        public int? StatusId { get; set; }
        public int? CategoryId { get; set; }
        public string Search { get; set; }
    }
}
