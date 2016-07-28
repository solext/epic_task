using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using TestWindow.Events;
using TestWindow.WinAPIAndHooks;

namespace TestWindow.ViewModel
{
    /// <summary>
    /// /
    /// </summary>
    public class StickerWindowVM : INotifyPropertyChanged
    {
        private TargetWindowEvents targetWindowEvents;
        private StickerWindowEvents stickerWindowEvents;
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
        public enum StickerPositionType
        {
            Left,
            Right
        }
        private StickerPositionType _StickerPosition { get; set; }

        public StickerPositionType StikerPosition
        {
            get
            {
                return _StickerPosition;
            }
            set
            {
                _StickerPosition = value; PropertyChanged(this, new PropertyChangedEventArgs("StikerPosition"));
                stickerWindowEvents.StickToTargetWindow(_HWND, StikerPosition);
            }
        }
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
                stickerWindowEvents.StickToTargetWindow(_HWND, StikerPosition);
                WinApi.GlobalWindowEvent += Win32WindowEvents_GlobalWindowEvent;
                WinApi.StartListening(_HWND);
            }
        }
        public StickerWindowVM(Window stickerWindow)
        {
            WindowEvents.InitializationStickerWindow(stickerWindow);
            targetWindowEvents = new TargetWindowEvents();
            stickerWindowEvents = new StickerWindowEvents();
            stickerWindow.StateChanged += stickerWindowEvents.StateChange;
            RunningAps = new Dictionary<IntPtr, string>();
            UpdateAppsCmd = new Command(UpdateApps);
            UpdateApps(null);
        }
        
        private void Win32WindowEvents_GlobalWindowEvent(int process,
            TargetWindow window, WinApiAdditionalTypes.EventTypes type)
        {
            //Console.WriteLine(type + "@ " + DateTime.Now.ToString("hh:mm.ss.fff") + window.ToString() + "\n");
            switch (type)
            {
                case WinApiAdditionalTypes.EventTypes.EVENT_SYSTEM_MOVESIZEEND:
                    break;
                case WinApiAdditionalTypes.EventTypes.EVENT_OBJECT_STATECHANGE:
                    break;
                case WinApiAdditionalTypes.EventTypes.EVENT_SYSTEM_MINIMIZESTART:
                    targetWindowEvents.MinimazeStart();
                    break;
                case WinApiAdditionalTypes.EventTypes.EVENT_SYSTEM_MINIMIZEEND:
                    targetWindowEvents.MinimazeEnd();
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
