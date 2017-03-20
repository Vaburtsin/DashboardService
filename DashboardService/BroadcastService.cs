using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardService
{
    internal class BroadcastService
    {
        private readonly static Lazy<BroadcastService> _instance = new Lazy<BroadcastService>(() => new BroadcastService(
            //GlobalHost.ConnectionManager.GetHubContext<StockTickerHub>().Clients
            Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<DashboardHub>()
        ));

        private Microsoft.AspNet.SignalR.IHubContext _hubContext;

        public BroadcastService(Microsoft.AspNet.SignalR.IHubContext hubContext)
        {
            _hubContext = hubContext;

            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public static BroadcastService Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private readonly DashboardModel _dashboard = new DashboardModel();
        public DashboardModel Dashboard { get { return _dashboard; } }


        PerformanceCounter cpuCounter;
        PerformanceCounter ramCounter;
        internal void Update()
        {
            _dashboard.Cpu = cpuCounter.NextValue();
            _dashboard.Ram = ramCounter.NextValue();
        }

        internal void Broadcast()
        {
            _hubContext.Clients.All.ServerSendSomeData(_dashboard);
        }
    }
}
