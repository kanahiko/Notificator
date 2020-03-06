﻿using StopScreenGoingDark;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;

/// <summary>
/// Class implementing support for "minimize to tray" functionality.
/// </summary>
public static class MinimizeToTray
{
    /// <summary>
    /// Enables "minimize to tray" behavior for the specified Window.
    /// </summary>
    /// <param name="window">Window to enable the behavior for.</param>
    public static void Enable(MainWindow window)
    {
        // No need to track this instance; its event handlers will keep it alive
        new MinimizeToTrayInstance(window);
    }

    /// <summary>
    /// Class implementing "minimize to tray" functionality for a Window instance.
    /// </summary>
    private class MinimizeToTrayInstance
    {
        private MainWindow _window;
        private NotifyIcon _notifyIcon;
        private bool _balloonShown;

        /// <summary>
        /// Initializes a new instance of the MinimizeToTrayInstance class.
        /// </summary>
        /// <param name="window">Window instance to attach to.</param>
        public MinimizeToTrayInstance(MainWindow window)
        {
            //Debug.WriteLine("1");
            Debug.Assert(window != null, "window parameter is null.");
            _window = window;
            _window.StateChanged += new EventHandler(HandleStateChanged);
            //Minimize();
        }

        /// <summary>
        /// Handles the Window's StateChanged event.
        /// </summary>
        /// <param name="sender">Event source.</param>
        /// <param name="e">Event arguments.</param>
        private void HandleStateChanged(object sender, EventArgs e)
        {
            if (_notifyIcon == null)
            {
                // Initialize NotifyIcon instance "on demand"
                _notifyIcon = new NotifyIcon();
                _notifyIcon.Icon = Icon.ExtractAssociatedIcon(Assembly.GetEntryAssembly().Location);

                MenuItem exitMenuItem = new MenuItem("Настройки");
                exitMenuItem.Click += ExitMenuItemClicked;
                _notifyIcon.ContextMenu = new ContextMenu(new System.Windows.Forms.MenuItem[] { exitMenuItem });

                _notifyIcon.MouseClick += new MouseEventHandler(HandleNotifyIconOrBalloonDoubleClicked);
                //_notifyIcon.BalloonTipClicked += new EventHandler(HandleNotifyIconOrBalloonDoubleClicked);
                //_notifyIcon.MouseRightClick += new MouseEventHandler(HandleNotifyIconOrBalloonClicked);
            }
            // Update copy of Window Title in case it has changed
            _notifyIcon.Text = _window.Title;

            // Show/hide Window and NotifyIcon
            var minimized = (_window.WindowState == WindowState.Minimized);
            _window.ShowInTaskbar = !minimized;
            _notifyIcon.Visible = minimized;
            if (minimized && !_balloonShown)
            {
                // If this is the first time minimizing to the tray, show the user what happened
                //_notifyIcon.ShowBalloonTip(1000, null, _window.Title, ToolTipIcon.None);
                _balloonShown = true;
            }
        }
        /// <summary>
        /// Handles a click on the notify icon or its balloon.
        /// </summary>
        /// <param name="sender">Event source.</param>
        /// <param name="e">Event arguments.</param>
        private void ExitMenuItemClicked(object sender, EventArgs e)
        {
            _window.WindowState = WindowState.Normal;
            _window.WindowState = WindowState.Normal;
            //System.Windows.Application.Current.Shutdown();
        }

        private void HandleNotifyIconOrBallooClicked(object sender, EventArgs e)
        {
            // Restore the Window
            //_window.WindowState = WindowState.Normal;
        }

        private void HandleNotifyIconOrBalloonDoubleClicked(object sender, EventArgs e)
        {
            // Restore the Window
            // _window.WindowState = WindowState.Normal;
        }
    }
}