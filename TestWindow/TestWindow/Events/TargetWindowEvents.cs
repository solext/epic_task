using System.Windows;

namespace TestWindow.Events
{
    public class TargetWindowEvents : WindowEvents
    {
        public void MinimazeStart()
        {
            _stickerWindow.WindowState = WindowState.Minimized;
        }
        public void MinimazeEnd()
        {
            _stickerWindow.WindowState = WindowState.Normal;
        }
    }
}
