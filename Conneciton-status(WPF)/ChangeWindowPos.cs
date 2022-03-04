using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Conneciton_status_WPF_
{
    class ChangeWindowPos
    {
        public static double WindowPosX { get; set; }
        public static double WindowPosY { get; set; }
        public static bool Dragable { get; set; }

        public static void ChangePos(double x, double y)
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double taskbarHeight = System.Windows.SystemParameters.PrimaryScreenHeight - System.Windows.SystemParameters.WorkArea.Height;
            if (Properties.Settings.Default.AllignToTaskBar)
            {
                App.Current.MainWindow.Top = screenHeight - taskbarHeight - App.Current.MainWindow.Height - y;
                App.Current.MainWindow.Left = screenWidth - App.Current.MainWindow.Width - x;
            }
            else
            {
                App.Current.MainWindow.Top = screenHeight - App.Current.MainWindow.Height - y;
                App.Current.MainWindow.Left = screenWidth - App.Current.MainWindow.Width - x;
            }
        }
    }
}
