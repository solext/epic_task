using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Interop;

namespace TestWindow.WinAPIAndHooks
{
    internal class WinApi
    {
        static IntPtr _otherWindowHook;
        static IntPtr _currentWindowHook;
        private static IntPtr _hwnd;
        static readonly WinApiFunctions.WinEventDelegate ProcDelegate = WinEventProc;

        public delegate void WinEvent(int process, TargetWindow window, WinApiAdditionalTypes.EventTypes type);
        public static event WinEvent GlobalWindowEvent;

        public static void StartListening(IntPtr hwnd)
        {
            _hwnd = hwnd;
            TargetWindow window = new TargetWindow(hwnd);
            if (_otherWindowHook != IntPtr.Zero) StopListening();
            _otherWindowHook = WinApiFunctions.SetWinEventHook(
                (uint)WinApiAdditionalTypes.EventTypes.EVENT_MIN,
                (uint)WinApiAdditionalTypes.EventTypes.EVENT_MAX,
                IntPtr.Zero,
                ProcDelegate,
                (uint)window.Process,
                0,
                (uint)(WinApiAdditionalTypes.WinHookParameter.OUTOFCONTEXT));
            TargetWindow currWin = new TargetWindow(new WindowInteropHelper(Application.Current.MainWindow).Handle);
            _currentWindowHook = WinApiFunctions.SetWinEventHook(
                (uint)WinApiAdditionalTypes.EventTypes.EVENT_SYSTEM_CAPTURESTART,
                (uint)WinApiAdditionalTypes.EventTypes.EVENT_SYSTEM_CAPTUREEND,
                IntPtr.Zero,
                ProcDelegate,
                (uint)currWin.Process,
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
            if (hwnd != _hwnd && hwnd != new WindowInteropHelper(Application.Current.MainWindow).Handle) return;
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
            WinApiFunctions.UnhookWinEvent(_otherWindowHook);
            WinApiFunctions.UnhookWinEvent(_currentWindowHook);
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
            var hDesktop = WinApiFunctions.GetDesktopWindow();
            return System.Windows.Forms.Screen.FromHandle(hDesktop).WorkingArea.Width;
        }
        public static double GetDisplayHight()
        {
            var hDesktop = WinApiFunctions.GetDesktopWindow();
            return System.Windows.Forms.Screen.FromHandle(hDesktop).WorkingArea.Height;
        }
        public static void MinimizeWindow()
        {
            WinApiFunctions.CloseWindow(_hwnd);
        }
    }



}
