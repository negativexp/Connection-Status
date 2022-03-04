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

namespace Conneciton_status_WPF_
{
    /// <summary>
    /// Interakční logika pro SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            
        }
        // LOAD
        private void SettingsWindow_Loaded(object sender, EventArgs e)
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            // ping
            TextBoxPingHost.Text = Properties.Settings.Default.PingHost;
            TextBoxInterval.Text = Convert.ToString(Properties.Settings.Default.PingInterval);
            // window
            SliderWindowOpacity.Value = Properties.Settings.Default.WindowOpacity;
            // slider pos x,y
            SliderPosX.Maximum = screenWidth - App.Current.MainWindow.Width;
            SliderPosY.Maximum = screenHeight - App.Current.MainWindow.Height;
            SliderPosX.Value = Properties.Settings.Default.WindowPosX;
            SliderPosY.Value = Properties.Settings.Default.WindowPosY;
            //allign box
            if(Properties.Settings.Default.AllignToTaskBar)
            {
                CheckBoxAllignToTaskBar.IsChecked = true;
            }
            else
            {
                CheckBoxAllignToTaskBar.IsChecked = false;
            }
        }
        static double taskbarHeight = System.Windows.SystemParameters.PrimaryScreenHeight - System.Windows.SystemParameters.WorkArea.Height;
        private void CheckBoxAllignToTaskBar_Checked(object sender, RoutedEventArgs e)
        {
            SliderPosY.Minimum = taskbarHeight;
        }
        private void CheckBoxAllignToTaskBar_Unchecked(object sender, RoutedEventArgs e)
        {
            SliderPosY.Minimum = 0;
        }
        private void SliderPosX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ChangeWindowPos.WindowPosX = SliderPosX.Value;
            ChangeWindowPos.ChangePos(ChangeWindowPos.WindowPosX, ChangeWindowPos.WindowPosY);
        }

        private void SliderPosY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Properties.Settings.Default.AllignToTaskBar)
            {
                ChangeWindowPos.WindowPosY = SliderPosY.Value - taskbarHeight;
            }
            else
            {
                ChangeWindowPos.WindowPosY = SliderPosY.Value;
            }
            ChangeWindowPos.ChangePos(ChangeWindowPos.WindowPosX, ChangeWindowPos.WindowPosY);
        }

        private void SliderWindowOpacity_Changed(object sender, RoutedEventArgs e)
        {
            ChangeWindowOpacity.Change(SliderWindowOpacity.Value);
        }
        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void ButtonResumePing_Click(object sender, RoutedEventArgs e)
        {
            GetPing.IsPaused = false;
        }
        private void ButtonPausePing_Click(object sender, RoutedEventArgs e)
        {
            GetPing.IsPaused = true;
        }
        private void ButtonX_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            KeyboardHook.Pressed2 = false;
        }

        private void Border_MouseDown(object sender, MouseEventArgs e)
        {
            DragMove();
        }
        private void TextBoxPingHost_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Properties.Settings.Default.PingHost = TextBoxPingHost.Text;
            }
            catch
            {
            }
        }

        private void TextBoxInterval_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Properties.Settings.Default.PingInterval = Convert.ToInt32(TextBoxInterval.Text);
            }
            catch
            {
            }
        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            // window opacity
            Properties.Settings.Default.WindowOpacity = SliderWindowOpacity.Value;
            // window pos
            Properties.Settings.Default.WindowPosX = SliderPosX.Value;
            Properties.Settings.Default.WindowPosY = SliderPosY.Value;
            // checkbox allign
            if ((bool)CheckBoxAllignToTaskBar.IsChecked)
            {
                Properties.Settings.Default.AllignToTaskBar = true;
            }
            else
            {
                Properties.Settings.Default.AllignToTaskBar = false;
            }
            Properties.Settings.Default.Save();
        }
        private void CheckBoxDragable_Checked(object sender, RoutedEventArgs e)
        {
            ChangeWindowPos.Dragable = true;
        }
        private void CheckBoxDragable_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeWindowPos.Dragable = false;
        }
    }
}
