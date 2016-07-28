using System;
using System.Windows;
using TestWindow.WinAPIAndHooks;
using static TestWindow.ViewModel.StickerWindowVM;

namespace TestWindow.Events
{
    public class StickerWindowEvents : WindowEvents
    { 
        public void StateChange(object sender, EventArgs e)
        {
            switch (_stickerWindow.WindowState)
            {
                case WindowState.Minimized:
                    
                    break;
                case WindowState.Normal:

                    break;
            }
        }

        internal void StickToTargetWindow(IntPtr _HWND, StickerPositionType StickerPosition)
        {
            TargetWindow _targetWindow = new TargetWindow(_HWND);
            switch (StickerPosition)
            {
                case StickerPositionType.Left:
                    _stickerWindow.Left = _targetWindow.Position.left - _stickerWindow.ActualWidth;
                    _stickerWindow.Top = _targetWindow.Position.top;
                    break;
                case StickerPositionType.Right:
                    _stickerWindow.Left = _targetWindow.Position.left + Math.Abs(_targetWindow.Width);
                    _stickerWindow.Top = _targetWindow.Position.top;
                    break;

            }
        }
    }
}
