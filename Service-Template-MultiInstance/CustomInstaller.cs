using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Template_MultiInstance
{
    [RunInstaller(true)]
    public partial class CustomInstaller : System.Configuration.Install.Installer
    {
        
        public CustomInstaller() : base()
        {
            InitializeComponent();
        }

        public override void Commit(IDictionary savedState)
        {
            Context.LogMessage("\t COMMIT \n");
            base.Commit(savedState);
        }

        public override void Install(IDictionary stateSaver)
        {
            Context.LogMessage("\t INSTALL \n");
            base.Install(stateSaver);
        }

        public override void Rollback(IDictionary savedState)
        {
            Context.LogMessage("\t ROLLBACK \n");
            base.Rollback(savedState);
        }

        public override void Uninstall(IDictionary savedState)
        {
            Context.LogMessage("\t UNINSTALL \n");
            base.Uninstall(savedState);
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            Context.LogMessage("\t ON BEFORE INSTALL \n");

            /*
            Context.LogMessage("IDictionnary Elements: " + savedState.Count);
            Context.LogMessage("Context Parameters: " + Context.Parameters.Count);
            foreach (var s in Context.Parameters.Keys)
            {
                Context.LogMessage("\tKey: " + s.ToString() + " Value: " + Context.Parameters[s.ToString()]);
            }
            */

            DefineDynamicSettings();

            Context.LogMessage("Service Name: " + this.serviceInstaller.ServiceName);
            Context.LogMessage("Assembly Path: " + Context.Parameters["assemblypath"]);

            base.OnBeforeInstall(savedState);
        }

        protected override void OnBeforeUninstall(IDictionary savedState)
        {

            Context.LogMessage("\t ON BEFORE UNINSTALL \n");
            
            /*
            Context.LogMessage("IDictionnary Elements: " + savedState.Count);
            Context.LogMessage("Context Parameters: " + Context.Parameters.Count);
            foreach (var s in Context.Parameters.Keys)
            {
                Context.LogMessage("\tKey: " + s.ToString() + " Value: " + Context.Parameters[s.ToString()]);
            }
            */

            DefineDynamicSettings();

            Context.LogMessage("Service Name: " + this.serviceInstaller.ServiceName);
            Context.LogMessage("Assembly Path: " + Context.Parameters["assemblypath"]);

            base.OnBeforeUninstall(savedState);
        }

        //dynamic installer settings
        private void DefineDynamicSettings()
        {
            if (Context.Parameters.ContainsKey("servicename"))
            {
                var svcname = Context.Parameters["servicename"];
                this.serviceInstaller.ServiceName = svcname;
                this.serviceInstaller.DisplayName = svcname;

                //define the executable path to store the name of the service within the service itself
                //this is because the servicename of the installer and of the service must be the same
                Context.Parameters["assemblypath"] = string.Format("\"{0}\" \"{1}\"", Context.Parameters["assemblypath"], svcname);
            }
        }
    }
}
