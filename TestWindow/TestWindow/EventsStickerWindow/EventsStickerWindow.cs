using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace TestWindow.EventsStickerWindow
{
    public static class EventsStickerWindow
    {
        private static Window _stickerWindow;
        public static void InitializationStickerWindow(Window stickerWindow)
        {
            _stickerWindow = stickerWindow;
        }
        public static void MinimazeStart()
        {
            _stickerWindow.WindowState = WindowState.Minimized;
        }
        public static void MinimazeEnd()
        {
            _stickerWindow.WindowState = WindowState.Normal;
        }
    }
}
