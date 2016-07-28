using System;
using System.Windows;

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
        
    }
}
