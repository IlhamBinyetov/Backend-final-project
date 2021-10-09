using QuarterTemplate.Data;
using QuarterTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }


        public Setting GetSetting()
        {
            return _context.Settings.FirstOrDefault();
        }

        public SocialNetwork GetSocialNetwork()
        {
            return _context.SocialNetworks.FirstOrDefault();
        }
    }
}
