using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using TestTestWindow.WinAPIAndHooks;
using TestWindow.WinAPIAndHooks;
using static TestWindow.WinAPIAndHooks.WinAPIAdditionalTypes;

namespace TestWindow.ViewModel
{
    public class StickerWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public Command UpdateAppsCmd { get; set; }

        //private Window _StickerWindow { get; set; }
        //public Window StickerWindow
        //{
        //    get
        //    {
        //        return _StickerWindow;
        //    }
        //    set
        //    {
        //        _StickerWindow = value; PropertyChanged(this, new PropertyChangedEventArgs("StickerWindow"));
        //    }
        //}

        private Dictionary<IntPtr, string> _RunningAps { get; set; }
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
        public Process SelectedProcess
        {
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
        public StickerWindowVM(Window stickerWindow)
        {
            EventsStickerWindow.EventsStickerWindow.InitializationStickerWindow(stickerWindow);
            this.RunningAps = new Dictionary<IntPtr, string>();
            this.UpdateAppsCmd = new Command(UpdateApps);
            UpdateApps(null);
        }

        private void Win32WindowEvents_GlobalWindowEvent(int Process,
            WindowPosition Window, EventTypes type)
        {
            Console.WriteLine(type + "@ " + DateTime.Now.ToString("hh:mm.ss.fff") + Window.ToString() + "\n");
            switch (type)
            {
                case EventTypes.EVENT_SYSTEM_MOVESIZEEND:
                    break;
                case EventTypes.EVENT_OBJECT_STATECHANGE:
                    break;
                case EventTypes.EVENT_SYSTEM_MINIMIZESTART:
                    EventsStickerWindow.EventsStickerWindow.MinimazeStart();
                    break;
                case EventTypes.EVENT_SYSTEM_MINIMIZEEND:
                    EventsStickerWindow.EventsStickerWindow.MinimazeEnd();
                    break;
            }
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
