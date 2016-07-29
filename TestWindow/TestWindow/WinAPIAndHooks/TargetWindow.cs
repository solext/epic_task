using System;

namespace TestWindow.WinAPIAndHooks
{
    internal class TargetWindow
    {
        private IntPtr _hWnd;

        /// <summary>
        /// dgfvdsgfe
        /// </summary>
        /// <param name="hWnd"></param>
        public TargetWindow(IntPtr hWnd)
        {
            _hWnd = new IntPtr(hWnd.ToInt32());
        }

        public int Width
        {
            get
            {
                return Position.right - Position.left;
            }
        }

        public int Height
        {
            get { return Position.bottom - Position.top; }
        }


        public WinApiAdditionalTypes.RECT Position
        {
            get
            {
                WinApiAdditionalTypes.RECT rect;
                WinApiFunctions.GetWindowRect(_hWnd, out rect);
                return rect;

            }


        }

        public int Process
        {
            get
            {
                int pid;
                WinApiFunctions.GetWindowThreadProcessId(_hWnd, out pid);
                return pid;
            }
        }

    }
}
