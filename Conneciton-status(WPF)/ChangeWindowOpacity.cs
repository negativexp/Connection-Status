using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conneciton_status_WPF_
{
    class ChangeWindowOpacity
    {
        public static void Change(double value)
        {
            App.Current.MainWindow.Background.Opacity = value / 100;
        }
    }
}
