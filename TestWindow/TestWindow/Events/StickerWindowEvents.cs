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

    }
}
