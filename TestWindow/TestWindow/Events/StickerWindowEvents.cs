using System;
using System.Windows;
using TestWindow.ViewModel;
using TestWindow.WinAPIAndHooks;

namespace TestWindow.Events
{
    public class StickerWindowEvents
    {
        private readonly Window _stickerWindow;
        public IntPtr Hwnd;
        public StickerWindowVM.StickerPositionType StickerPosition;

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
                    WinApi.MinimizeWindow();
                    break;
                }
                case WindowState.Normal:
                {
                    if (Hwnd != IntPtr.Zero)
                    {
                        WinApi.RestoreWindow();
                    }
                    break;
                }
                case WindowState.Maximized:
                    {
                        _stickerWindow.LocationChanged -= this.LocationChanged;
                        _stickerWindow.StateChanged -= this.StateChange;
                        _stickerWindow.SizeChanged -= this.SizeChanged;
                        LocationChangeMaximazed();
                        _stickerWindow.LocationChanged += this.LocationChanged;
                        _stickerWindow.StateChanged += this.StateChange;
                        _stickerWindow.SizeChanged += this.SizeChanged;
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
            if (Hwnd == IntPtr.Zero) return;
            if (_stickerWindow.Left<0-_stickerWindow.Width) return;
            TargetWindow targetWindow=new TargetWindow(Hwnd);
            if (StickerPosition == StickerWindowVM.StickerPositionType.Left)
            {
                double left = _stickerWindow.Left+_stickerWindow.Width-15;
                
                WinApiFunctions.SetWindowPos(Hwnd, 0,(int) left, (int) _stickerWindow.Top,
                    targetWindow.Width, (int)_stickerWindow.Height, 
                    (uint)WinApiAdditionalTypes.SetWindowPosFlags.SWP_NOACTIVATE);
            }
            else
            {
                double left = _stickerWindow.Left -targetWindow.Width;
                WinApiFunctions.SetWindowPos(Hwnd, 0, (int)left+15, (int)_stickerWindow.Top,
                    targetWindow.Width, (int)_stickerWindow.Height,
                    (uint)WinApiAdditionalTypes.SetWindowPosFlags.SWP_NOACTIVATE);
            }
            _stickerWindow.Height = targetWindow.Height;
        }
        public void LocationChangeMaximazed()
        {
            _stickerWindow.WindowState = WindowState.Normal;
            if (Hwnd == IntPtr.Zero) return;
            TargetWindow targetWindow = new TargetWindow(Hwnd);
            _stickerWindow.Height = WinApi.GetDisplayHight() - 30;
            _stickerWindow.Top = 0;
            if (StickerPosition == StickerWindowVM.StickerPositionType.Left)
            {
                _stickerWindow.Left = 0;
                
                double left = _stickerWindow.ActualWidth;

                WinApiFunctions.SetWindowPos(Hwnd, 0, (int)left, (int)_stickerWindow.Top,
                    (int)(WinApi.GetDisplayWidth() - left), (int)_stickerWindow.Height,
                    (uint)WinApiAdditionalTypes.SetWindowPosFlags.SWP_NOACTIVATE);
            }
            else
            {
                _stickerWindow.Left = WinApi.GetDisplayWidth() - _stickerWindow.ActualWidth + 20;
                double left = 0;
                WinApiFunctions.SetWindowPos(Hwnd, 0, (int)left, (int)_stickerWindow.Top,
                    (int)(WinApi.GetDisplayWidth() - _stickerWindow.ActualWidth + 20), (int)_stickerWindow.Height,
                    (uint)WinApiAdditionalTypes.SetWindowPosFlags.SWP_NOACTIVATE);
            }
            
        }
    }
}
