using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;

namespace TestWindow.ViewModel
{
    public class StickerWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public Command UpdateAppsCmd { get; set; }

        private List<string> _RunningAps { get; set; }
        public List<string> RunningAps
        {
            get
            {
                return _RunningAps;
            }
            set
            {
                _RunningAps = value; PropertyChanged(this, new PropertyChangedEventArgs("RunningAps"));
            }
        }
        private Process _SelectedProcess { get; set; }
        public Process SelectedProcess {
            get
            {
                return _SelectedProcess;
            }
            set
            {
                _SelectedProcess = value; PropertyChanged(this, new PropertyChangedEventArgs("SelectedProcess"));
            }
        }
        private string _SelectedProcessStr { get; set; }
        public string SelectedProcessStr
        {
            get
            {
                return _SelectedProcessStr;
            }
            set
            {
                _SelectedProcessStr = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedProcessStr"));
                SelectedProcess = Process.GetProcessesByName(SelectedProcessStr)[0];
                Win32WindowEvents.StartListening(Convert.ToUInt32(SelectedProcess.Id));
            }
        }
        public StickerWindowVM()
        {
            this.RunningAps = new List<string>();
            this.UpdateAppsCmd = new Command(UpdateApps);
            UpdateApps(null);
            Win32WindowEvents.GlobalWindowEvent += Win32WindowEvents_GlobalWindowEvent;
        }


        void Win32WindowEvents_GlobalWindowEvent(Process Process, Win32Window Window, Win32WindowEvents.EventTypes type)
        {
                Console.WriteLine(type + "@ " + DateTime.Now.ToString("hh:mm.ss.fff") + Window.ToString() + "\n");
        }

        private void UpdateApps(object obj)
        {
            Process[] MyProcess = Process.GetProcesses().OrderBy(x => x.ProcessName).Where(x => x.MainWindowHandle != IntPtr.Zero).ToArray();
            for (int i = 0; i < MyProcess.Length; i++)
                RunningAps.Add(MyProcess[i].ProcessName);


            //comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            //treeView1.AfterSelect += treeView1_AfterSelect;
            //comboBox1.SelectedIndex = 0;
        }
    }
}
