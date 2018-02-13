using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Service_Template_MultiInstance
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            //for multi-instance services, register a command-line <path to service>.exe "instancename"
            //installer should use "instancename" and as display name "ServiceName-instancename"
            Environment.GetCommandLineArgs();

            var service = new Service(args);

            ServiceBase.Run(service);
        }
    }
}
