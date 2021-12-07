using System;
using System.Windows.Forms;

namespace Wiz.Commands
{
    public class ShowMainWindowCommand : BaseCommand
    {
        public ShowMainWindowCommand(Action<object?>? execute, Predicate<object?>? canExecute) : base(execute, canExecute) { }

        public static void Show(object? parameter)
        {
            var mainWindow = new MainWindow();

            var mousePosition = Control.MousePosition;
            mainWindow.Top = mousePosition.Y - mainWindow.Height;
            mainWindow.Left = mousePosition.X - mainWindow.Width / 2;

            mainWindow.Show();
            mainWindow.Activate();
        }
    }
}
