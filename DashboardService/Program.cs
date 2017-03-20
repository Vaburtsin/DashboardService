using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace DashboardService
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<DashboardService>(sc =>
                {
                    sc.ConstructUsing(() => new DashboardService());
                    sc.WhenStarted(s => s.Start());
                    sc.WhenStopped(s => s.Stop());
                });
                x.RunAsPrompt();

                x.SetServiceName("DashboardService");
                x.SetDescription("Service Dashboard");
                x.SetDisplayName("Dashboard Service");
            });
        }
    }
}
