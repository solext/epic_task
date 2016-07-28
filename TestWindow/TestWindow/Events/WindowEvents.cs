using System.Windows;

namespace TestWindow.Events
{
    public abstract class WindowEvents
    {
        protected static Window _stickerWindow;
        public static void InitializationStickerWindow(Window stickerWindow)
        {
            _stickerWindow = stickerWindow;
        }
        
    }
}
