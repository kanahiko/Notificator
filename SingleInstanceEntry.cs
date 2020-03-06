using Microsoft.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 public class SingleInstanceEntry//: ISingleInstanceApp
{
 /*       private const string Unique = "Change this to something that uniquely identifies your program.";
        
    public static void SingleInstancess()
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
    {*//*
                // Bring window to foreground
                if (this.MainWindow.WindowState == WindowState.Minimized)
                {
                    this.MainWindow.WindowState = WindowState.Normal;
                }

                this.MainWindow.Activate();*//*
        return true;
    }*/
}

