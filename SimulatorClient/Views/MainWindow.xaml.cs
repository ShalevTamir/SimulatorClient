using SimulatorClient.Services;
using SimulatorClient.ViewModel;
using SimulatorClient.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimulatorClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PopupWindowsService _popupWindowService;
        private WindowControlsService _windowControlsService;
        public MainWindow()
        {
            _popupWindowService = PopupWindowsService.Instance;
            _windowControlsService = WindowControlsService.Instance;
            WindowViewModel viewModel = new WindowViewModel();
            this.DataContext = viewModel;
            InitializeComponent();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            _windowControlsService.Minimize(this);
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            _windowControlsService.Maximize(this);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void AddTeleParameterClick(object sender, RoutedEventArgs e)
        {
            _popupWindowService.PopupWindow<AddParameterWindow>();
        }
    }
}
