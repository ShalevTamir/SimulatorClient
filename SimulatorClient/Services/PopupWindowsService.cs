using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;

namespace SimulatorClient.Services
{
    internal class PopupWindowsService
    {
        private static PopupWindowsService _instance;
        public static PopupWindowsService Instance
        {
            get
            {
                _instance ??= new PopupWindowsService();
                return _instance;
            }
            private set { }
        }

        private List<Window> windowInstances;

        private PopupWindowsService()
        {
            windowInstances = new List<Window>();
        }
        public Window? GetWindow<WindowType>() where WindowType : Window
        {
            return FindWindow<WindowType>();
        }

        public void PopupWindow<WindowType>() where WindowType : Window, new()
        {
            var windowInstance = FindWindow<WindowType>();
            if (windowInstance is null || !windowInstance.IsLoaded)
            {
                windowInstances.Remove(windowInstance);
                windowInstance = new WindowType();
                windowInstances.Add(windowInstance);
            }

            if (!windowInstance.IsLoaded)
            {
                windowInstance.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                windowInstance.Show();  
            }
            else
            {
                windowInstance.Focus();
            }
        }

        private Window? FindWindow<WindowType>() where WindowType : Window
        {
            var windowInstance = windowInstances.Find(window => window is WindowType);
            return windowInstance;
        }
    }
}
