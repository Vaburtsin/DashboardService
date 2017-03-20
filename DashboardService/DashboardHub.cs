using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardService
{
    public class DashboardHub : Microsoft.AspNet.SignalR.Hub
    {
        private readonly BroadcastService _micenService = BroadcastService.Instance;

        public DashboardModel GetDashboard()
        {
            return _micenService.Dashboard;
        }
    }
}
