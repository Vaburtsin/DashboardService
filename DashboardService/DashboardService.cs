using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardService
{
    internal class DashboardService
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger("DashboardService");
        
        private readonly System.Timers.Timer _timerBroadcast;
        private IDisposable _signalR;

        internal DashboardService()
        {
            _timerBroadcast = new System.Timers.Timer(2000) { AutoReset = false };
            _timerBroadcast.Elapsed += new System.Timers.ElapsedEventHandler(TimerBroadcast_Elapsed);
        }

        public void Start()
        {
            string url = ConfigurationManager.AppSettings["ServiceEntryUrl"];
            _timerBroadcast.Interval = int.Parse(ConfigurationManager.AppSettings["TimerInterval"]) * 1000;

            _signalR = Microsoft.Owin.Hosting.WebApp.Start(url);
            //-- When WebApp.Start launch an access dienied
            /* To add url reservation for an account :
             * netsh http add urlacl url=http://*:8043/ user=DOMAIN\USER
             * To remove url reservation :
             * netsh http delete urlacl url=http://*:8043/
             */

            _timerBroadcast.Start();

            string startMessage = String.Format("DashboardService Started with url:{0}", url);
            _log.Debug(startMessage);
            Console.WriteLine(startMessage);
        }

        public void Stop()
        {
            _timerBroadcast.Stop();
            _timerBroadcast.Dispose();
            _signalR.Dispose();

            _log.Debug("DashboardService Stoped");
        }

        private void TimerBroadcast_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                BroadcastService.Instance.Update();
                BroadcastService.Instance.Broadcast();
            }
            catch (Exception ex)
            {
                _log.Error("Fatal error in DashboardService.", ex);
            }
            finally
            {
                _timerBroadcast.Start();
            }
        }
    }
}
