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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;

namespace Conneciton_status_WPF_
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        System.Timers.Timer timerPing = new System.Timers.Timer(Properties.Settings.Default.PingInterval);
        void StatusWindow_Loaded(object sender, RoutedEventArgs e)
        {
            double taskbarHeight = System.Windows.SystemParameters.PrimaryScreenHeight - System.Windows.SystemParameters.WorkArea.Height;
            KeyboardHook hook = new KeyboardHook();
            GetPing ping = new GetPing();
            this.Topmost = true;
            //window pos
            if (Properties.Settings.Default.AllignToTaskBar)
            {
                ChangeWindowPos.ChangePos(Properties.Settings.Default.WindowPosX, Properties.Settings.Default.WindowPosY - taskbarHeight);
            }
            else
            {
                ChangeWindowPos.ChangePos(Properties.Settings.Default.WindowPosX, Properties.Settings.Default.WindowPosY);
            }
            MessageBox.Show(Properties.Settings.Default.WindowPosX.ToString() + " ; " + Properties.Settings.Default.WindowPosY.ToString());

            //opacity
            ChangeWindowOpacity.Change(Properties.Settings.Default.WindowOpacity);
            //hook
            hook.SetHook();
            //timers
            SetTimerPing();
            SetTimerIsConnected();
            ping.SetWorkerPing();
            
        }

        // PING PONG
        public void SetTimerPing()
        {
            timerPing.Elapsed += new ElapsedEventHandler(TimerPingElapsed);
            timerPing.Enabled = true;
        }
        
        private void TimerPingElapsed(object sender, ElapsedEventArgs e)
        {
            //zmeni interval
            timerPing.Interval = Properties.Settings.Default.PingInterval;

            this.Dispatcher.Invoke(() =>
            {
                if (GetPing.PingLatency != 0)
                {
                    if(GetPing.IsPaused)
                    {
                        TextBlockPing.Text = "Ping: Paused";
                    }
                    else
                    {
                        TextBlockPing.Text = "Ping: " + GetPing.PingLatency;
                    }
                }
                else
                {
                    TextBlockPing.Text = "Ping: ERROR";
                }
            });
        }
        // END

        // IS CONNECTED
        void SetTimerIsConnected()
        {
            System.Timers.Timer timerIsConnected;
            timerIsConnected = new System.Timers.Timer();
            timerIsConnected.Elapsed += new ElapsedEventHandler(TimerIsConnectedElapsed);
            timerIsConnected.Interval = 1000;
            timerIsConnected.Enabled = true;
        }
        private void TimerIsConnectedElapsed(object sender, ElapsedEventArgs e)
        {
            if(IsConnected.CheckIfConnected())
            {
                this.Dispatcher.Invoke(() =>
                {
                    TextBlockConnection.Text = "   CONNECTED";
                    TextBlockConnection.Foreground = Brushes.Green;
                });
            }
            else
            {
                this.Dispatcher.Invoke(() =>
                {
                    TextBlockConnection.Text = "DISCONNECTED";
                    TextBlockConnection.Foreground = Brushes.Red;
                });
            }
        }
        // END
        private void StatusWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(ChangeWindowPos.Dragable)
            {
                DragMove();
            }
        }
    }
}
