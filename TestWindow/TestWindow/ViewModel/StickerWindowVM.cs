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
        private static Window _stickerWindow;
        private TargetWindowEvents _targetWindowEvents;
        private static StickerWindowEvents _stickerWindowEvents;
        private bool _isTicked;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public static bool IsMoving { get; private set; }

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
                    _stickerWindowEvents.StickerPosition = StickerPosition;
                    _isTicked = false;
                    _targetWindowEvents.LocationChange(_HWND, StickerPosition, _isTicked);
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
                _stickerWindowEvents.Hwnd = _HWND;
                _stickerWindowEvents.StickerPosition = StickerPosition;
                WinApi.GlobalWindowEvent += WindowEvents;
                WinApi.StartListening(_HWND);
                _targetWindowEvents.LocationChange(_HWND, StickerPosition, false);
                _isTicked = true;

            }
        }


        public StickerWindowVM(Window stickerWindow)
        {
            _stickerWindow = stickerWindow;
            _targetWindowEvents = new TargetWindowEvents(stickerWindow);
            _stickerWindowEvents = new StickerWindowEvents(stickerWindow);
            AddEvents();
            stickerWindow.Closed += stickerWindow_Closed;
            RunningAps = new Dictionary<IntPtr, string>();
            UpdateAppsCmd = new Command(UpdateApps);
            UpdateApps(null);
        }
        public static void AddEvents()
        {
            _stickerWindow.LocationChanged += _stickerWindowEvents.LocationChanged;
            _stickerWindow.StateChanged += _stickerWindowEvents.StateChange;
            _stickerWindow.SizeChanged += _stickerWindowEvents.SizeChanged;
            _stickerWindow.MouseLeftButtonUp+= _stickerWindowEvents.MouseLeftButtonDown;
        }

        public static void RemoveEvents()
        {
            _stickerWindow.LocationChanged -= _stickerWindowEvents.LocationChanged;
            _stickerWindow.StateChanged -= _stickerWindowEvents.StateChange;
            _stickerWindow.SizeChanged -= _stickerWindowEvents.SizeChanged;
            _stickerWindow.MouseLeftButtonUp -= _stickerWindowEvents.MouseLeftButtonDown;
        }

        private void stickerWindow_Closed(object sender, EventArgs e)
        {
            WinApiFunctions.SendMessage(_HWND.ToInt32(), 0x0112, 0xF060, 0);
            WinApi.StopListening();
        }

        private void WindowEvents(int process,
            TargetWindow window, WinApiAdditionalTypes.EventTypes type)
        {
            Console.WriteLine(  IsMoving);
            if (!WinApiFunctions.IsWindowVisible(_HWND))
            {
                _stickerWindow.Close();
            }
            switch (type)
            {
                case WinApiAdditionalTypes.EventTypes.EVENT_OBJECT_LOCATIONCHANGE:
                    {
                        RemoveEvents();
                        _targetWindowEvents.LocationChange(_HWND, StickerPosition, _isTicked);
                        AddEvents();
                        break;
                    }
                case WinApiAdditionalTypes.EventTypes.EVENT_SYSTEM_MINIMIZESTART:
                {
                    _targetWindowEvents.MinimazeStart();
                    break;
                }
                case WinApiAdditionalTypes.EventTypes.EVENT_SYSTEM_MINIMIZEEND:
                    _targetWindowEvents.MinimazeEnd();
                    break;
                case WinApiAdditionalTypes.EventTypes.EVENT_SYSTEM_CAPTURESTART:
                    IsMoving = true;
                    break;
                case WinApiAdditionalTypes.EventTypes.EVENT_SYSTEM_CAPTUREEND:
                    IsMoving = false;
                    break;
            }
        }

        private void UpdateApps(object obj)
        {
            RunningAps = WinApi.GetOpenWindows();
        }
    }
}
