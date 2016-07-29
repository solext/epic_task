using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using TestWindow.Events;
using TestWindow.WinAPIAndHooks;

namespace TestWindow.ViewModel
{
    public class StickerWindowVM : INotifyPropertyChanged
    {
        private Window _stickerWindow;
        private TargetWindowEvents targetWindowEvents;
        private StickerWindowEvents stickerWindowEvents;
        private bool _isTicked;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public Command UpdateAppsCmd { get; set; }
        public enum StickerPositionType
        {
            Left,
            Right
        }
        private StickerPositionType _StickerPosition { get; set; }

        public StickerPositionType StickerPosition
        {
            get
            {
                return _StickerPosition;
            }
            set
            {
                _StickerPosition = value;
                PropertyChanged(this, new PropertyChangedEventArgs("StickerPosition"));
                if (_HWND != IntPtr.Zero)
                {
                    WinApi.StartListening(_HWND);
                    stickerWindowEvents.StickerPosition = StickerPosition;
                    _isTicked = false;
                    targetWindowEvents.LocationChange(_HWND, StickerPosition, _isTicked);
                    _isTicked = true;
                }
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
                stickerWindowEvents.Hwnd = _HWND;
                stickerWindowEvents.StickerPosition = StickerPosition;
                WinApi.GlobalWindowEvent += Win32WindowEvents_GlobalWindowEvent;
                WinApi.StartListening(_HWND);
                targetWindowEvents.LocationChange(_HWND, StickerPosition, false);
                _isTicked = true;

            }
        }


        public StickerWindowVM(Window stickerWindow)
        {
            _stickerWindow = stickerWindow;
            targetWindowEvents = new TargetWindowEvents(stickerWindow);
            stickerWindowEvents = new StickerWindowEvents(stickerWindow);
            stickerWindow.LocationChanged += stickerWindowEvents.LocationChanged;
            stickerWindow.StateChanged += stickerWindowEvents.StateChange;
            stickerWindow.SizeChanged += stickerWindowEvents.SizeChanged;
            stickerWindow.Closed += stickerWindow_Closed;
            RunningAps = new Dictionary<IntPtr, string>();
            UpdateAppsCmd = new Command(UpdateApps);
            UpdateApps(null);
        }

        private void stickerWindow_Closed(object sender, EventArgs e)
        {
            WinApiFunctions.SendMessage(_HWND.ToInt32(), (uint)0x0112, 0xF060, 0);
            WinApi.StopListening();
        }

        private void Win32WindowEvents_GlobalWindowEvent(int process,
            TargetWindow window, WinApiAdditionalTypes.EventTypes type)
        {
            if (!WinApiFunctions.IsWindowVisible(_HWND))
            {
                _stickerWindow.Close();
            }
            switch (type)
            {
                case WinApiAdditionalTypes.EventTypes.EVENT_OBJECT_LOCATIONCHANGE:
                    {
                        _stickerWindow.LocationChanged -= stickerWindowEvents.LocationChanged;
                        _stickerWindow.StateChanged -= stickerWindowEvents.StateChange;
                        _stickerWindow.SizeChanged -= stickerWindowEvents.SizeChanged;
                        targetWindowEvents.LocationChange(_HWND, StickerPosition, _isTicked);
                        _stickerWindow.LocationChanged += stickerWindowEvents.LocationChanged;
                        _stickerWindow.StateChanged += stickerWindowEvents.StateChange;
                        _stickerWindow.SizeChanged += stickerWindowEvents.SizeChanged;
                        break;
                    }
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
            RunningAps = WinApi.GetOpenWindows();
        }
    }
}
