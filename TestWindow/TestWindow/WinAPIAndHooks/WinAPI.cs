using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TestWindow.WinAPIAndHooks;

namespace TestTestWindow.WinAPIAndHooks
{
    internal class WinApi
    {
        static IntPtr hhook;
        private static IntPtr _hwnd;
        static WinAPIFunctions.WinEventDelegate procDelegate = new WinAPIFunctions.WinEventDelegate(WinEventProc);

        public delegate void WinEvent(int Process, WindowPosition Window, WinAPIAdditionalTypes.EventTypes type);
        public static event WinEvent GlobalWindowEvent;

        public static void StartListening(IntPtr hwnd)
        {
            _hwnd = hwnd;
            WindowPosition Window = new WindowPosition(hwnd);
            hhook = WinAPIFunctions.SetWinEventHook((uint)WinAPIAdditionalTypes.EventTypes.EVENT_MIN, (uint)WinAPIAdditionalTypes.EventTypes.EVENT_MAX, IntPtr.Zero,
                    procDelegate, (uint)Window.Process, 0, (uint)(WinAPIAdditionalTypes.WinHookParameter.OUTOFCONTEXT));
        }
        static void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            if (hwnd == _hwnd)
            {
                WindowPosition Window = new WindowPosition(hwnd);

                ////if window is found fire event
                //if (predicate != null && predicate.Invoke(Window) == true)
                //{
                //    WindowFound(Window.Process, Window, (EventTypes)eventType);
                //    predicate = null;
                //    StopListening();
                //}

                if (GlobalWindowEvent != null)
                {
                    GlobalWindowEvent(Window.Process, Window, (WinAPIAdditionalTypes.EventTypes) eventType);
                }
            }
        }
        public static void StopListening()
        {
            WinAPIFunctions.UnhookWinEvent(hhook);
        }

        public static Dictionary<IntPtr, string> GetOpenWindows()
        {
            var lShellWindow = WinAPIFunctions.GetShellWindow();
            var lWindows = new Dictionary<IntPtr, string>();

            WinAPIFunctions.EnumWindows(delegate(IntPtr hWnd, int lParam)
            {
                if (hWnd == lShellWindow) return true;
                if (!WinAPIFunctions.IsWindowVisible(hWnd)) return true;

                var lLength = WinAPIFunctions.GetWindowTextLength(hWnd);
                if (lLength == 0) return true;

                var lBuilder = new StringBuilder(lLength);
                WinAPIFunctions.GetWindowText(hWnd, lBuilder, lLength + 1);

                lWindows[hWnd] = lBuilder.ToString();
                return true;
            }, 0);
            return lWindows;
        }
    }



}
