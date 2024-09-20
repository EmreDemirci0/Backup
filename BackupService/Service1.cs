using Backup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BackupService
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Logger.WriteToLog("Servis Başladı: ");
            timer = new Timer(10000);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Start();
        }

        protected override void OnStop()
        {
            Logger.WriteToLog("Servis Durduruldu: ");
            timer.Stop();
        }

        private  void OnElapsedTime(object sender, ElapsedEventArgs e)
        {
            Logger.WriteToLog("Servis Devam: ");
        }

    }
}
