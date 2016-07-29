using System;
using System.Windows;
using TestWindow.ViewModel;
using TestWindow.WinAPIAndHooks;

namespace TestWindow.Events
{
    public class TargetWindowEvents 
    {
        static Window _stickerWindow;

        public TargetWindowEvents(Window stickerWindow)
        {
            _stickerWindow = stickerWindow;
        }
        public void MinimazeStart()
        {
            _stickerWindow.WindowState = WindowState.Minimized;
        }
        public void MinimazeEnd()
        {
            _stickerWindow.WindowState = WindowState.Normal;
        }
        public void LocationChange(IntPtr hwnd, StickerWindowVM.StickerPositionType stickerPosition, bool isTicked)
        {
            if (WinApiFunctions.IsIconic(hwnd)) return;
            TargetWindow targetWindow = new TargetWindow(hwnd);
            switch (stickerPosition)
            {
                case StickerWindowVM.StickerPositionType.Left:
                    {
                        double left = targetWindow.Position.left - _stickerWindow.ActualWidth+15;
                        if (left < 0 && !isTicked)
                        {
                            WinApiFunctions.SetWindowPos(hwnd, 0, (int)_stickerWindow.Width+15, targetWindow.Position.top, 0, 0,
                                (uint)WinApiAdditionalTypes.SetWindowPosFlags.SWP_NOSIZE);
                            _stickerWindow.Left = 0;
                        }
                        else
                        {
                            _stickerWindow.Left = left;
                        }
                        _stickerWindow.Top = targetWindow.Position.top;
                        _stickerWindow.Height = targetWindow.Height;
                        break;
                    }
                case StickerWindowVM.StickerPositionType.Right:
                    {
                        double left = targetWindow.Position.left + Math.Abs(targetWindow.Width)-15;
                        double displayWidth = WinApi.GetDisplayWidth();
                        if (left + _stickerWindow.Width >= displayWidth && !isTicked)
                        {
                            WinApiFunctions.SetWindowPos(hwnd, 0, 
                                (int)(displayWidth - _stickerWindow.Width - targetWindow.Width)-15,
                                targetWindow.Position.top, 0, 0,
                                (uint)WinApiAdditionalTypes.SetWindowPosFlags.SWP_NOSIZE);
                            _stickerWindow.Left = displayWidth - _stickerWindow.Width;
                        }
                        else
                        {
                            _stickerWindow.Left = left;
                        }
                        _stickerWindow.Top = targetWindow.Position.top;
                        _stickerWindow.Height = targetWindow.Height;
                        break;
                    }
            }
        }
    }
}
