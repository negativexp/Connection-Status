using System.Net.NetworkInformation;
using System.Windows.Media;

namespace Conneciton_status_WPF_
{
    class IsConnected
    {
        MainWindow mainwindow = new MainWindow();
        public static bool CheckIfConnected()
        {
            bool connection = NetworkInterface.GetIsNetworkAvailable();
            if (connection)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
