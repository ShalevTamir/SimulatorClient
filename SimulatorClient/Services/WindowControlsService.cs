using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimulatorClient.Services
{
    class WindowControlsService
    {
        private static WindowControlsService _instance;
        public static WindowControlsService Instance
        {
            get
            {
                _instance ??= new WindowControlsService();
                return _instance;
            }
        }
        private WindowControlsService() 
        { 
        }

        public void Maximize(Window windowToMaximize)
        {
            windowToMaximize.WindowState = windowToMaximize.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized; ;
        }

        public void Close(Window windowToClose)
        {
            windowToClose.Close();
        }

        public void Minimize(Window windowToMinimize)
        {
            windowToMinimize.WindowState = WindowState.Minimized;
        }
    }
}
