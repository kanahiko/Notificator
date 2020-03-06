using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StopScreenGoingDark
{
    class NotificationController
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();

        DateTime currentDate;

        public Action ShowNotification;
        public Action HideNotification;
        //public int notificationT;
        int sleepT;

        int timeToSleep;

        bool isInactive;
        public NotificationController(int notificationTime, int sleepTime)
        {
            isInactive = false;
            currentDate = DateTime.Now;
            //notificationT = notificationTime * 60 * 1000;
            sleepT = sleepTime * 60 * 1000;
            timeToSleep = 60000;/*sleepT
                          - (Environment.TickCount
                          - (int)Win32.GetLastInputTime())
                          - MainWindow.notificationTime * 60 * 1000;*/
            /*if (timeToSleep < 0)
            {
                timeToSleep = sleepT - MainWindow.notificationTime * 60 * 1000;
            }*/
            System.Diagnostics.Debug.WriteLine(timeToSleep / 1000);
            //notificationT = 5000;
            //sleepT = 10000;
        }


        public void StartWorker()
        {
            if (!worker.IsBusy)
            {
                worker.DoWork += worker_DoWork;
                worker.WorkerReportsProgress = true;
                worker.ProgressChanged += Worker_ProgressChanged;
                worker.RunWorkerAsync();
            }
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("timeToSleep");
            int envTicks = Environment.TickCount;
            long lastInputTime = envTicks - Win32.GetLastInputTime();

            System.Diagnostics.Debug.WriteLine($"last input time {(lastInputTime) / 1000}");
            //System.Diagnostics.Debug.WriteLine($"slepT-notT {(sleepT - notificationT * 60 * 1000) / 1000}");
            //System.Diagnostics.Debug.WriteLine(envTicks-lastInputTime);

            if (lastInputTime >= (sleepT - MainWindow.notificationTime * 60 * 1000) && lastInputTime <= sleepT)
            {
                if (!isInactive)
                {
                    ShowNotification();
                }
                timeToSleep = 2000;// notificationT;
                isInactive = true;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"weqe {(lastInputTime)}");
                System.Diagnostics.Debug.WriteLine($"weqe {(int)(lastInputTime % 60000)}");
                System.Diagnostics.Debug.WriteLine($"weqe {60000 - (int)(lastInputTime % 60000)}");
                if (lastInputTime > sleepT)
                {
                    timeToSleep = sleepT - (int)lastInputTime - MainWindow.notificationTime * 60 * 1000;
                }
                else
                {
                    timeToSleep = 60000 - (int)(lastInputTime % 60000);
                }
                /*timeToSleep = sleepT - (int)lastInputTime - MainWindow.notificationTime * 60 * 1000; ;
                if (timeToSleep < 0)
                {
                    timeToSleep = sleepT - MainWindow.notificationTime * 60 * 1000; ;
                }*/
                HideNotification();
                isInactive = false;
            }

            System.Diagnostics.Debug.WriteLine($"timeToSleep {(timeToSleep) / 1000}");
            //ShowNotification();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                System.Diagnostics.Debug.WriteLine($"!!timeToSleep {(timeToSleep) / 1000}");
                Thread.Sleep(timeToSleep);
                //Thread.Sleep(900000);
                worker.ReportProgress(1);
                Thread.Sleep(2000);
            }
        }

    }
}