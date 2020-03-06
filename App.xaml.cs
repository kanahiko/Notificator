using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Shell;
using Microsoft.Win32;

namespace StopScreenGoingDark
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application, ISingleInstanceApp
    {
        private const string Unique = "Change this to something that uniquely identifies your program.UNIWUQPROGRAM";
        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public static void Main()
        {
            if (Microsoft.Shell.SingleInstance<App>.InitializeAsFirstInstance(Unique))
            {
                StopScreenGoingDark.App app = new StopScreenGoingDark.App();
                app.InitializeComponent();
                app.Run();
                //RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                //registryKey.SetValue("ApplicationName", System.Reflection.Assembly.GetExecutingAssembly().Location);

                // Allow single instance code to perform cleanup operations
                Microsoft.Shell.SingleInstance<App>.Cleanup();
            }
        }

        public bool SignalExternalCommandLineArgs(System.Collections.Generic.IList<string> args)
        {/*
            // Bring window to foreground
            if (this.MainWindow.WindowState == WindowState.Minimized)
            {
                this.MainWindow.WindowState = WindowState.Normal;
            }

            this.MainWindow.Activate();*/
            return true;
        }
    }
}
