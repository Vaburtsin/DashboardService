using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardService
{
    public class DashboardModel
    {
        public string RefreshDateTime
        {
            get
            {
                return string.Format("{0:dd MMMM yyyy} à {0:HH:mm:ss}", DateTime.Now);
            }
        }

        public float Cpu { get; set; }

        public float CpuAvail { get { return 100 - Cpu; } }

        public float Ram { get; set; }

        public string Tick
        {
            get
            {
                return string.Format("{0:mm:ss}", DateTime.Now);
            }
        }
    }
}
