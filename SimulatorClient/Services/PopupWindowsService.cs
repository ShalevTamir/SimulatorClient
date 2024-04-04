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

        public void PopupWindow<WindowType>() where WindowType : Window, new()
        {
            var windowInstance = windowInstances.Find(window => window is WindowType);
            if (windowInstance == null || !windowInstance.IsLoaded)
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
    }
}
