using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SyncDonThuService
{
    partial class SyncDonThuService : ServiceBase
    {
        private Timer timer = null;
        public SyncDonThuService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            this.timer = new Timer(); // milliseconds 
            timer.Interval = int.Parse(ConfigurationSettings.AppSettings["TimeSync"]); //30 phút
            this.timer.AutoReset = true;
            this.timer.Elapsed += new ElapsedEventHandler(this.timer_Elapsed);
            this.timer.Start();
            Utilities.WriteLog("Sync DonThu Service Started");
        }

        public void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                string TimeStart = ConfigurationSettings.AppSettings["TimeStart"].ToString();
                System.Threading.Thread.Sleep(5000);
                if (DateTime.Now.ToString("HH:mm") == TimeStart)
                new SyncDonThu().Sync();
            }
            catch (Exception ex)
            {
                Utilities.WriteLog(ex);
            }
        }
        protected override void OnStop()
        {
            Utilities.WriteLog("Sync DonThu Service Stoped");
        }
    }
}
