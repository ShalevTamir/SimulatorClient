using SimulatorClient.Services;
using SimulatorClient.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace SimulatorClient.Views
{
    /// <summary>
    /// Interaction logic for AddConstraintWindow.xaml
    /// </summary>
    public partial class AddConstraintWindow : Window
    {
        WindowControlsService _windowControlsService;
        public AddConstraintWindow()
        {
            _windowControlsService = WindowControlsService.Instance;
            AddConstraintVIewModel viewModel = new AddConstraintVIewModel();
            DataContext = viewModel;
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
            _windowControlsService.Close(this);
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
