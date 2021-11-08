using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using QuarterTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterTemplate
{
    
    public class QuarterHub:Hub
    {
        private readonly UserManager<AppUser> _userManager;

        public QuarterHub(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        public override Task OnConnectedAsync()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                AppUser user = _userManager.FindByNameAsync(Context.User.Identity.Name).Result;
                user.ConnectionId = Context.ConnectionId;

                var result = _userManager.UpdateAsync(user).Result;
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                AppUser user = _userManager.FindByNameAsync(Context.User.Identity.Name).Result;
                user.ConnectionId = null;
                user.LastConnectedDate = DateTime.UtcNow.AddHours(4);

                var result = _userManager.UpdateAsync(user).Result;
            }
            return base.OnDisconnectedAsync(exception);
        }
    }
}
