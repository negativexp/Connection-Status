using System;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Threading;

namespace Conneciton_status_WPF_
{
    class GetPing
    {
        public static int PingLatency{ get; set; }
        public static bool IsPaused { get; set; }

        static BackgroundWorker BackgroundWorkerPing = new BackgroundWorker();
        public void SetWorkerPing()
        {
            BackgroundWorkerPing.DoWork += new DoWorkEventHandler(backgroundWorkerPing_DoWork);
            BackgroundWorkerPing.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorkerPing_Done);
            BackgroundWorkerPing.WorkerSupportsCancellation = true;
            BackgroundWorkerPing.RunWorkerAsync();
        }

        private void backgroundWorkerPing_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(Properties.Settings.Default.PingInterval);
            Ping ping = new Ping();
            if (!IsPaused)
            {
                try
                {
                    if(Properties.Settings.Default.PingHost == null)
                    {

                    }
                    else
                    {
                        PingReply reply = ping.Send(Properties.Settings.Default.PingHost, 1000);
                        if (reply != null)
                        {
                            PingLatency = Convert.ToInt32(reply.RoundtripTime);
                        }
                        else
                        {
                            PingLatency = Convert.ToInt32(reply.RoundtripTime);
                        }
                    }
                }
                catch (Exception)
                {
                    PingLatency = 0;
                } 
            }
            else
            {
                //...
            }
        }
        private void backgroundWorkerPing_Done(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorkerPing.RunWorkerAsync();
        }
    }
}