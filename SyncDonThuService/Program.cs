﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SyncDonThuService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //new SyncDonThu().Sync();
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new SyncDonThuService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
