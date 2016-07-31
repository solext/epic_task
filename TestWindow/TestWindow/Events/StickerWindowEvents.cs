using System;
using System.Windows;
using System.Windows.Input;
using TestWindow.ViewModel;
using TestWindow.WinAPIAndHooks;

namespace TestWindow.Events
{
    public class StickerWindowEvents
    {
        private readonly Window _stickerWindow;
        private IntPtr _hwnd;
        private StickerWindowVM.StickerPositionType _stickerPosition;
        public IntPtr Hwnd
        {
            get { return _hwnd; }
            set { _hwnd = value; }
        }

        public StickerWindowVM.StickerPositionType StickerPosition
        {
            get { return _stickerPosition; }
            set { _stickerPosition = value; }
        }

        public StickerWindowEvents(Window stickerWindow)
        {
            _stickerWindow = stickerWindow;
        }


        public void StateChange(object sender, EventArgs e)
        {
            switch (_stickerWindow.WindowState)
            {
                case WindowState.Minimized:
                    {
                        if (StickerWindowVM.IsMoving)
                        {
                            _stickerWindow.WindowState = WindowState.Normal;
                            WinApi.RestoreWindow();
                            break;
                        }
                        WinApi.MinimizeWindow();
                        break;
                    }
                case WindowState.Normal:
                    {
                        if (_hwnd != IntPtr.Zero)
                        {
                            WinApi.RestoreWindow();
                        }
                        break;
                    }
                case WindowState.Maximized:
                    {
                        StickerWindowVM.RemoveEvents();
                        LocationChangeMaximazed();
                        StickerWindowVM.AddEvents();
                        break;
                    }
            }
        }

        public void LocationChanged(object sender, EventArgs e)
        {
            LocationChange();
        }

        public void SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LocationChange();
        }
        public void LocationChange()
        {
            if (_hwnd == IntPtr.Zero) return;
            if (_stickerWindow.Left < 0 - _stickerWindow.Width) return;
            TargetWindow targetWindow = new TargetWindow(_hwnd);
            if (targetWindow.Position.left < 0 - targetWindow.Width) return;
            double left;
            if (_stickerPosition == StickerWindowVM.StickerPositionType.Left)
            {
                left = _stickerWindow.Left + _stickerWindow.Width - 15;
            }
            else
            {
                left = _stickerWindow.Left - targetWindow.Width + 15;
            }

            WinApiFunctions.SetWindowPos(_hwnd, 0, (int)left, (int)_stickerWindow.Top,
                   targetWindow.Width, (int)_stickerWindow.Height,
                   (uint)WinApiAdditionalTypes.SetWindowPosFlags.SWP_NOACTIVATE);
            _stickerWindow.Height = targetWindow.Height;
        }
        public void LocationChangeMaximazed()
        {
            //_stickerWindow.WindowState = WindowState.Normal;
            if (_hwnd == IntPtr.Zero) return;
            _stickerWindow.Height = WinApi.GetDisplayHight();
            _stickerWindow.Top = 0;
            if (_stickerPosition == StickerWindowVM.StickerPositionType.Left)
            {
                _stickerWindow.Left = 0;

                double left = _stickerWindow.ActualWidth;

                WinApiFunctions.SetWindowPos(_hwnd, 0, (int)left, 0,
                    (int)(WinApi.GetDisplayWidth() - left + 20), (int)_stickerWindow.Height,
                    (uint)WinApiAdditionalTypes.SetWindowPosFlags.SWP_NOACTIVATE);
            }
            else
            {
                _stickerWindow.Left = WinApi.GetDisplayWidth() - _stickerWindow.ActualWidth;
                double left = 0;
                WinApiFunctions.SetWindowPos(_hwnd, 0, (int)left, 0,
                    (int)(WinApi.GetDisplayWidth() - _stickerWindow.ActualWidth + 20), (int)_stickerWindow.Height,
                    (uint)WinApiAdditionalTypes.SetWindowPosFlags.SWP_NOACTIVATE);
            }

        }

        public void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WinApi.RestoreWindow();
        }

    }
}
