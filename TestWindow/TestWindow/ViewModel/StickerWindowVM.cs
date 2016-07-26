using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using TestTestWindow.WinAPIAndHooks;
using TestWindow.WinAPIAndHooks;

namespace TestWindow.ViewModel
{
    public class StickerWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public Command UpdateAppsCmd { get; set; }

        private Dictionary<IntPtr,string> _RunningAps { get; set; }
        public Dictionary<IntPtr, string> RunningAps
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
        private IntPtr _HWND { get; set; }
        public IntPtr HWND
        {
            get
            {
                return _HWND;
            }
            set
            {
                _HWND = value;
                PropertyChanged(this, new PropertyChangedEventArgs("HWND"));
                //SelectedProcess = Process.GetProcessesByName(SelectedProcessStr)[0];
                WinApi.GlobalWindowEvent += Win32WindowEvents_GlobalWindowEvent;
                WinApi.StartListening(_HWND);
            }
        }
        public StickerWindowVM()
        {
            this.RunningAps = new Dictionary<IntPtr, string>();
            this.UpdateAppsCmd = new Command(UpdateApps);
            UpdateApps(null);
        }


        private void Win32WindowEvents_GlobalWindowEvent(int Process,
            WindowPosition Window, WinAPIAdditionalTypes.EventTypes type)
        {
                Console.WriteLine(type + "@ " + DateTime.Now.ToString("hh:mm.ss.fff") + Window.ToString() + "\n");
        }

        private void UpdateApps(object obj)
        {
            //Process[] MyProcess = Process.GetProcesses().OrderBy(x => x.ProcessName).Where(x => x.MainWindowHandle != IntPtr.Zero).ToArray();
            //for (int i = 0; i < MyProcess.Length; i++)
            //    RunningAps.Add(MyProcess[i].ProcessName);
            RunningAps = WinApi.GetOpenWindows();

            //comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            //treeView1.AfterSelect += treeView1_AfterSelect;
            //comboBox1.SelectedIndex = 0;
        }
    }
}
