using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFNotification.Core.Configuration;
using WPFNotification.Model;
using WPFNotification.Services;

namespace StopScreenGoingDark
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NotificationDialogService dailogService;
        NotificationConfiguration notificationConfiguration;
        Notification notification;
        NotificationController notificationController;

        KeyboardListener KListener;

        public static int notificationTime;
        int sleepTime = 15;
        System.Media.SoundPlayer player;

        public MainWindow()
        {
            InitializeComponent();
            MinimizeToTray.Enable(this);

        }

        private void Window_Activated(object sender, EventArgs e)
        {
            WindowState = WindowState.Minimized;

            slider.Maximum = sleepTime - 1;
            labelMax.Content = (sleepTime - 1).ToString();

            string notificationTimeString = ConfigurationManager.AppSettings.Get("NotificationTime");
            if (notificationTimeString == null)
            {
                notificationTime = 1;
                ConfigurationManager.AppSettings.Set("NotificationTime", notificationTime.ToString());
            }
            else
            {
                notificationTime = int.Parse(notificationTimeString);
            }
            slider.Value = notificationTime;

            dailogService = new NotificationDialogService();
            notification = new Notification();
            notification.ImgURL = "pack://application:,,,/warning.png";
            notification.Title = $"Компьютер скоро уйдёт в сон";
            notification.Message = "Пошевелите мышкой или нажмите кнопку";
            player = new System.Media.SoundPlayer(Properties.Resources.music);
            //player.FileName = "music.wav";
            //player = new System.Media.SoundPlayer("pack://application:,,,/music.wav");
            notificationConfiguration = new NotificationConfiguration(new TimeSpan(0, sleepTime - notificationTime, 0), 350, 150, "", null);

            notificationController = new NotificationController(notificationTime, sleepTime);
            notificationController.ShowNotification = ShowNotification;
            notificationController.HideNotification = HideNotification;
            notificationController.StartWorker();

        }

        private void HideNotification()
        {
            Console.WriteLine("hide");
            player.Stop();
            dailogService.ClearNotifications();
        }

        void KListener_KeyDown(object sender, RawKeyEventArgs args)
        {
            Console.WriteLine(args.Key.ToString());
        }

        public void ShowNotification()
        {
            //SystemSounds.Beep.Play();
            System.Diagnostics.Debug.WriteLine("here");
            player.Play();
            dailogService.ShowNotificationWindow(notification, notificationConfiguration);
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WindowState = WindowState.Minimized;
            SaveNotificationSettings();
            e.Cancel = true;
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            notificationTime = (int)slider.Value;
            currentValueLabel.Content = notificationTime.ToString();
            /*if (notificationController != null)
            {
                notificationController.notificationT = notificationTime;
            }*/
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
            SaveNotificationSettings();
        }

        void SaveNotificationSettings()
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings["NotificationTime"] == null)
                {
                    settings.Add("NotificationTime", notificationTime.ToString());
                }
                else
                {
                    settings["NotificationTime"].Value = notificationTime.ToString();
                }

                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
    
}
