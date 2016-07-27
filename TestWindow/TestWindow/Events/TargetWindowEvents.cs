using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interop;

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
