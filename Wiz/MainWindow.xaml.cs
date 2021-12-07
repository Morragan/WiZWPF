using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
using System.Windows.Threading;
using Wiz.Commands;
using Wiz.Helpers;

namespace Wiz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.Instance.MainWindowVM;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Close();
        }

        private void PowerButton_Click(object sender, RoutedEventArgs e)
        {
            if (App.Instance.MainWindowVM.Enabled) App.Instance.MainWindowVM.Enabled = false;
            else App.Instance.MainWindowVM.Enabled = true;
        }
    }
}
