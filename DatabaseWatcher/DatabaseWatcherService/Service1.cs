using DatabaseWatcherService.ClasseTeste;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TableWatcher;

namespace DatabaseWatcherService
{
    public partial class Service1 : ServiceBase
    {
        TableWatcherStrategy<Pessoa> TableWatcher;

        public Service1()
        {
            InitializeComponent();
            TableWatcher = new TableWatcherStrategy<Pessoa>();
        }

        protected override void OnStart(string[] args)
        {
            TableWatcher.InitializeTableWatcher();
            TableWatcher.StartTableWatcher();
        }

        protected override void OnStop()
        {
            TableWatcher.StopTableWatcher();
        }
    }
}
