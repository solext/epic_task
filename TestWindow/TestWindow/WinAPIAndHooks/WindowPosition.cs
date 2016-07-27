using System;

namespace TestWindow.WinAPIAndHooks
{
    internal class WindowPosition
    {
        public IntPtr hWnd;

        /// <summary>
        /// dgfvdsgfe
        /// </summary>
        /// <param name="hWnd"></param>
        public WindowPosition(IntPtr hWnd)
        {
            this.hWnd = new IntPtr(hWnd.ToInt32());
        }

        public int Width
        {
            get { return Position.right - Position.left; }
        }

        public int Height
        {
            get { return Position.bottom - Position.top; }
        }


        public WinAPIAdditionalTypes.RECT Position
        {
            get
            {
                WinAPIAdditionalTypes.RECT rect = new WinAPIAdditionalTypes.RECT();
                WinAPIFunctions.GetWindowRect(hWnd, out rect);
                return rect;

            }


        }

        public int Process
        {
            get
            {
                int pid;
                WinAPIFunctions.GetWindowThreadProcessId(hWnd, out pid);
                return pid;
            }
        }

    }
}
