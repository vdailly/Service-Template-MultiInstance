using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Service_Template_MultiInstance
{
    public partial class Service : ServiceBase
    {

        /* ressources used to build this template:
         * 
         * https://msdn.microsoft.com/fr-fr/library/system.configuration.install.installer(v=vs.110).aspx
         * https://msdn.microsoft.com/fr-fr/library/system.configuration.install.installcontext(v=vs.110).aspx
         * https://msdn.microsoft.com/fr-fr/library/system.diagnostics.eventloginstaller(v=vs.110).aspx
         * 
         * https://docs.microsoft.com/en-us/dotnet/framework/windows-services/walkthrough-creating-a-windows-service-application-in-the-component-designer
         * 
         * http://www.damirscorner.com/blog/posts/20140804-InstallNetWindowsServiceWithADifferentName.html
         * https://journalofasoftwaredev.wordpress.com/2008/07/16/multiple-instances-of-same-windows-service/
         * https://social.msdn.microsoft.com/Forums/vstudio/en-US/162c377f-2926-4a80-916b-69449c1e95b5/windows-service-dynamic-servicename?forum=csharpgeneral
         * https://diaryofadeveloper.wordpress.com/2012/05/
         * https://social.msdn.microsoft.com/Forums/vstudio/en-US/c937d1f4-cee9-4a0f-8271-27d62a90e3fa/windows-service-multiple-instances?forum=csharpgeneral
         * 
         * 
         * 
        */

        //added constants used as default value for service installation and runtime
        public const string DefaultName        = "Service";
        public const string DefaultDisplayName = "Service";
        public const string DefaultDescription = "";

        //define some example process properties used by the service
        Task serviceTask;
        bool stop;

        //updated ctor to allow parameters
        public Service(string[] args) : base()
        {
            InitializeComponent();

            //while Service Name is defined in InitializeComponent, this is override here.
            //this allow to add an installer on the service UI, but not necessary when knowing how to setup installer.
            ServiceName = DefaultName;

            if (args.Count() > 0)
            {
                ServiceName = args[0];
            }

            if (EventLog != null)
            {
                EventLog.WriteEntry(ServiceName + " is in the ctor with " + args.Count() + " arguments", EventLogEntryType.Information, 1000);
            }

           
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry(ServiceName + " is in the OnStart", EventLogEntryType.Information, 1000);

            // Arguments that must be present when the service is automatically started
            // can be placed in the ImagePath string value for the service's registry key
            // (HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\<service name> ). 
            // You can obtain the arguments from the registry using the GetCommandLineArgs method, 
            // for example: string[] imagePathArgs = Environment.GetCommandLineArgs();

            //example task
            serviceTask = new Task(Run);
            serviceTask.Start();   
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry(ServiceName + " is in the OnStop", EventLogEntryType.Information, 1000);
            stop = true;
            serviceTask.Wait();
        }

        //an example of a running process indefinitly
        private void Run()
        {
            while(true)
            {
                EventLog.WriteEntry(ServiceName + " is running", EventLogEntryType.Information, 1000);
                System.Threading.Thread.Sleep(10000);
                if (stop)
                {
                    return;
                }
            }
        }
    }
}
