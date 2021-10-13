using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Models
{
    public class Setting
    {
        public int Id { get; set; }
        public string HeaderLogo { get; set; }
        public string FooterLogo { get; set; }
        public string MainPhone { get; set; }
        public string SecondPhone { get; set; }
        public string MainEmail { get; set; }
        public string SecondEmail { get; set; }
        public string EmailLogo { get; set; }
        public string EmailImage { get; set; }
        public string PhoneImage { get; set; }
        public string PhoneLogo { get; set; }
        public string LocationLogo { get; set; }
        public string LocationImage { get; set; }
        public string Location1 { get; set; }
        public string Location2 { get; set; }
        public string Location3 { get; set; }
        public string BackgroundImage { get; set; }

        [NotMapped]
        public IFormFile HeaderImg { get; set; }

        [NotMapped]
        public IFormFile FooterImg { get; set; }

        [NotMapped]
        public IFormFile BgImg { get; set; }
    }
}
