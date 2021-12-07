using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using Wiz.Commands;
using Wiz.Helpers;
using Wiz.ViewModels;

namespace Wiz
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static App Instance => (App)Current;

        internal TaskbarIcon WizTaskbarIcon { get; private set; }
        internal Dictionary<string, object> ParamsToUpdate { get; private set; }
        internal DispatcherTimer Timer { get; private set; }
        internal MainWindowViewModel MainWindowVM { get; private set; }

        private readonly UdpClient udpClient;
        private IPEndPoint lightbulbEndpoint;

        public App()
        {
            MainWindowVM = new MainWindowViewModel();

            udpClient = new UdpClient();
            lightbulbEndpoint = new IPEndPoint(IPAddress.Parse("192.168.1.101"), 38899);
            udpClient.Connect(lightbulbEndpoint);

            ParamsToUpdate = new();

            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(100);
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            WizTaskbarIcon = (TaskbarIcon)FindResource("WizTaskbarIcon");
            WizTaskbarIcon.DoubleClickCommand = new ShowMainWindowCommand(ShowMainWindowCommand.Show, null);
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (ParamsToUpdate.Count == 0) return;

            var paramsString = CommandHelper.BuildParams(ParamsToUpdate, MainWindowVM);
            var message = $"{{\"method\":\"setPilot\",\"params\":{paramsString}}}";
            var byteMessage = Encoding.ASCII.GetBytes(message);
            var getStateMessage = Encoding.ASCII.GetBytes($"{{\"method\":\"getPilot\"}}");

            try
            {
                udpClient.Send(byteMessage, byteMessage.Length);
                string response = Encoding.ASCII.GetString(udpClient.Receive(ref lightbulbEndpoint));
                udpClient.Send(getStateMessage, getStateMessage.Length);
                string response2 = Encoding.ASCII.GetString(udpClient.Receive(ref lightbulbEndpoint));
                Debug.WriteLine(message);
                Debug.WriteLine(response);
                Debug.WriteLine(response2);
            }
            catch (Exception) { }

            ParamsToUpdate.Clear();
        }
    }
}
