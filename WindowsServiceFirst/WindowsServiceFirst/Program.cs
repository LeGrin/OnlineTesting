using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using SMPR_testing_Lib.Repository;

namespace SMPRShedulerService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ISmprRepository repo = new SmprRepository();
            ServicesToRun = new ServiceBase[] 
            { 
                new SMPRSheduler(repo) 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
