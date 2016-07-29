using System;
using System.Collections.Generic;
using System.Text;

namespace TestWindow.WinAPIAndHooks
{
    internal class WinApi
    {
        static IntPtr _hhook;
        private static IntPtr _hwnd;
        static readonly WinApiFunctions.WinEventDelegate ProcDelegate = WinEventProc;

        public delegate void WinEvent(int process, TargetWindow window, WinApiAdditionalTypes.EventTypes type);
        public static event WinEvent GlobalWindowEvent;

        public static void StartListening(IntPtr hwnd)
        {
            _hwnd = hwnd;
            TargetWindow window = new TargetWindow(hwnd);
            if (_hhook != IntPtr.Zero) StopListening();
            _hhook = WinApiFunctions.SetWinEventHook(
                (uint)WinApiAdditionalTypes.EventTypes.EVENT_MIN,
                (uint)WinApiAdditionalTypes.EventTypes.EVENT_MAX,
                IntPtr.Zero,
                ProcDelegate,
                (uint)window.Process,
                0,
                (uint)(WinApiAdditionalTypes.WinHookParameter.OUTOFCONTEXT));
           RestoreWindow();
        }

        public static void RestoreWindow()
        {
            WinApiFunctions.ShowWindow(
                           _hwnd, (int)WinApiAdditionalTypes.WindowShowStyle.Restore);

            WinApiFunctions.SetWindowPos(
                 _hwnd, 0, 0, 0, 0, 0,
                 (uint)(WinApiAdditionalTypes.SetWindowPosFlags.SWP_NOMOVE
                 | WinApiAdditionalTypes.SetWindowPosFlags.SWP_NOSIZE));
        }
        static void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            if (hwnd != _hwnd) return;
            var window = new TargetWindow(hwnd);

            ////if window is found fire event
            //if (predicate != null && predicate.Invoke(Window) == true)
            //{
            //    WindowFound(Window.Process, Window, (EventTypes)eventType);
            //    predicate = null;
            //    StopListening();
            //}

            if (GlobalWindowEvent != null)
            {
                GlobalWindowEvent(window.Process, window, (WinApiAdditionalTypes.EventTypes)eventType);
            }
        }
        public static void StopListening()
        {
            WinApiFunctions.UnhookWinEvent(_hhook);
        }

        public static Dictionary<IntPtr, string> GetOpenWindows()
        {
            var lShellWindow = WinApiFunctions.GetShellWindow();
            var lWindows = new Dictionary<IntPtr, string>();

            WinApiFunctions.EnumWindows(delegate(IntPtr hWnd, int lParam)
            {
                if (hWnd == lShellWindow) return true;
                if (!WinApiFunctions.IsWindowVisible(hWnd)) return true;

                var lLength = WinApiFunctions.GetWindowTextLength(hWnd);
                if (lLength == 0) return true;

                var lBuilder = new StringBuilder(lLength);
                WinApiFunctions.GetWindowText(hWnd, lBuilder, lLength + 1);

                lWindows[hWnd] = lBuilder.ToString();
                return true;
            }, 0);
            return lWindows;
        }

        public static double GetDisplayWidth()
        {
            WinApiAdditionalTypes.RECT desktop;
            IntPtr hDesktop = WinApiFunctions.GetDesktopWindow();
            WinApiFunctions.GetWindowRect(hDesktop, out desktop);
            return desktop.right;
        }
        public static double GetDisplayHight()
        {
            WinApiAdditionalTypes.RECT desktop;
            IntPtr hDesktop = WinApiFunctions.GetDesktopWindow();
            WinApiFunctions.GetWindowRect(hDesktop, out desktop);
            return desktop.bottom;
        }
        public static void MinimizeWindow()
        {
            WinApiFunctions.CloseWindow(_hwnd);
        }
    }



}
