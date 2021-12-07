using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Wiz.ResourceDictionaries
{
    public partial class WizTaskbarIcon : ResourceDictionary
    {
        public WizTaskbarIcon() { }

        private void ExitApp_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
