using System;

namespace TestWindow.WinAPIAndHooks
{
    internal class WinApiAdditionalTypes
    {
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        internal enum WinHookParameter : uint
        {
            INCONTEXT = 4,
            OUTOFCONTEXT = 0,
            SKIPOWNPROCESS = 2,
            SKIPOWNTHREAD = 1
        }

        internal enum EventTypes : uint
        {
            WINEVENT_OUTOFCONTEXT = 0x0000, // Events are ASYNC

            WINEVENT_SKIPOWNTHREAD = 0x0001, // Don't call back for events on installer's thread

            WINEVENT_SKIPOWNPROCESS = 0x0002, // Don't call back for events on installer's process

            WINEVENT_INCONTEXT = 0x0004, // Events are SYNC, this causes your dll to be injected into every process

            EVENT_MIN = 0x00000001,

            EVENT_MAX = 0xFFFFFFFF,

            EVENT_SYSTEM_SOUND = 0x0001,

            EVENT_SYSTEM_ALERT = 0x0002,

            EVENT_SYSTEM_FOREGROUND = 0x0003,

            EVENT_SYSTEM_MENUSTART = 0x0004,

            EVENT_SYSTEM_MENUEND = 0x0005,

            EVENT_SYSTEM_MENUPOPUPSTART = 0x0006,

            EVENT_SYSTEM_MENUPOPUPEND = 0x0007,

            EVENT_SYSTEM_CAPTURESTART = 0x0008,

            EVENT_SYSTEM_CAPTUREEND = 0x0009,

            EVENT_SYSTEM_MOVESIZESTART = 0x000A,

            EVENT_SYSTEM_MOVESIZEEND = 0x000B,

            EVENT_SYSTEM_CONTEXTHELPSTART = 0x000C,

            EVENT_SYSTEM_CONTEXTHELPEND = 0x000D,

            EVENT_SYSTEM_DRAGDROPSTART = 0x000E,

            EVENT_SYSTEM_DRAGDROPEND = 0x000F,

            EVENT_SYSTEM_DIALOGSTART = 0x0010,

            EVENT_SYSTEM_DIALOGEND = 0x0011,

            EVENT_SYSTEM_SCROLLINGSTART = 0x0012,

            EVENT_SYSTEM_SCROLLINGEND = 0x0013,

            EVENT_SYSTEM_SWITCHSTART = 0x0014,

            EVENT_SYSTEM_SWITCHEND = 0x0015,

            EVENT_SYSTEM_MINIMIZESTART = 0x0016,

            EVENT_SYSTEM_MINIMIZEEND = 0x0017,

            EVENT_SYSTEM_DESKTOPSWITCH = 0x0020,

            EVENT_SYSTEM_END = 0x00FF,

            EVENT_OEM_DEFINED_START = 0x0101,

            EVENT_OEM_DEFINED_END = 0x01FF,

            EVENT_UIA_EVENTID_START = 0x4E00,

            EVENT_UIA_EVENTID_END = 0x4EFF,

            EVENT_UIA_PROPID_START = 0x7500,

            EVENT_UIA_PROPID_END = 0x75FF,

            EVENT_CONSOLE_CARET = 0x4001,

            EVENT_CONSOLE_UPDATE_REGION = 0x4002,

            EVENT_CONSOLE_UPDATE_SIMPLE = 0x4003,

            EVENT_CONSOLE_UPDATE_SCROLL = 0x4004,

            EVENT_CONSOLE_LAYOUT = 0x4005,

            EVENT_CONSOLE_START_APPLICATION = 0x4006,

            EVENT_CONSOLE_END_APPLICATION = 0x4007,

            EVENT_CONSOLE_END = 0x40FF,

            EVENT_OBJECT_CREATE = 0x8000, // hwnd ID idChild is created item

            EVENT_OBJECT_DESTROY = 0x8001, // hwnd ID idChild is destroyed item

            EVENT_OBJECT_SHOW = 0x8002, // hwnd ID idChild is shown item

            EVENT_OBJECT_HIDE = 0x8003, // hwnd ID idChild is hidden item

            EVENT_OBJECT_REORDER = 0x8004, // hwnd ID idChild is parent of zordering children

            EVENT_OBJECT_FOCUS = 0x8005, // hwnd ID idChild is focused item

            EVENT_OBJECT_SELECTION = 0x8006, // hwnd ID idChild is selected item (if only one), or idChild is OBJID_WINDOW if complex

            EVENT_OBJECT_SELECTIONADD = 0x8007, // hwnd ID idChild is item added

            EVENT_OBJECT_SELECTIONREMOVE = 0x8008, // hwnd ID idChild is item removed

            EVENT_OBJECT_SELECTIONWITHIN = 0x8009, // hwnd ID idChild is parent of changed selected items

            EVENT_OBJECT_STATECHANGE = 0x800A, // hwnd ID idChild is item w/ state change

            EVENT_OBJECT_LOCATIONCHANGE = 0x800B, // hwnd ID idChild is moved/sized item

            EVENT_OBJECT_NAMECHANGE = 0x800C, // hwnd ID idChild is item w/ name change

            EVENT_OBJECT_DESCRIPTIONCHANGE = 0x800D, // hwnd ID idChild is item w/ desc change

            EVENT_OBJECT_VALUECHANGE = 0x800E, // hwnd ID idChild is item w/ value change

            EVENT_OBJECT_PARENTCHANGE = 0x800F, // hwnd ID idChild is item w/ new parent

            EVENT_OBJECT_HELPCHANGE = 0x8010, // hwnd ID idChild is item w/ help change

            EVENT_OBJECT_DEFACTIONCHANGE = 0x8011, // hwnd ID idChild is item w/ def action change

            EVENT_OBJECT_ACCELERATORCHANGE = 0x8012, // hwnd ID idChild is item w/ keybd accel change

            EVENT_OBJECT_INVOKED = 0x8013, // hwnd ID idChild is item invoked

            EVENT_OBJECT_TEXTSELECTIONCHANGED = 0x8014, // hwnd ID idChild is item w? test selection change

            EVENT_OBJECT_CONTENTSCROLLED = 0x8015,

            EVENT_SYSTEM_ARRANGMENTPREVIEW = 0x8016,

            EVENT_OBJECT_END = 0x80FF,

            EVENT_AIA_START = 0xA000,

            EVENT_AIA_END = 0xAFFF,
        }

        internal enum WindowShowStyle : int
        {
            Hide = 0,
            ShowNormal = 1,
            ShowMinimized = 2,
            ShowMaximized = 3,
            Maximize = 3,
            ShowNormalNoActivate = 4,
            Show = 5,
            Minimize = 6,
            ShowMinNoActivate = 7,
            ShowNoActivate = 8,
            Restore = 9,
            ShowDefault = 10,
            ForceMinimized = 11

        }

        [Flags]
        public enum SetWindowPosFlags : uint
        {
            /// <summary>
            ///     If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window. This prevents the calling thread from blocking its execution while other threads process the request.
            /// </summary>
            SWP_ASYNCWINDOWPOS = 0x4000,

            /// <summary>
            ///     Prevents generation of the WM_SYNCPAINT message.
            /// </summary>
            SWP_DEFERERASE = 0x2000,

            /// <summary>
            ///     Draws a frame (defined in the window's class description) around the window.
            /// </summary>
            SWP_DRAWFRAME = 0x0020,

            /// <summary>
            ///     Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
            /// </summary>
            SWP_FRAMECHANGED = 0x0020,

            /// <summary>
            ///     Hides the window.
            /// </summary>
            SWP_HIDEWINDOW = 0x0080,

            /// <summary>
            ///     Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
            /// </summary>
            SWP_NOACTIVATE = 0x0010,

            /// <summary>
            ///     Discards the entire contents of the client area. If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.
            /// </summary>
            SWP_NOCOPYBITS = 0x0100,

            /// <summary>
            ///     Retains the current position (ignores X and Y parameters).
            /// </summary>
            SWP_NOMOVE = 0x0002,

            /// <summary>
            ///     Does not change the owner window's position in the Z order.
            /// </summary>
            SWP_NOOWNERZORDER = 0x0200,

            /// <summary>
            ///     Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved. When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
            /// </summary>
            SWP_NOREDRAW = 0x0008,

            /// <summary>
            ///     Same as the SWP_NOOWNERZORDER flag.
            /// </summary>
            SWP_NOREPOSITION = 0x0200,

            /// <summary>
            ///     Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
            /// </summary>
            SWP_NOSENDCHANGING = 0x0400,

            /// <summary>
            ///     Retains the current size (ignores the cx and cy parameters).
            /// </summary>
            SWP_NOSIZE = 0x0001,

            /// <summary>
            ///     Retains the current Z order (ignores the hWndInsertAfter parameter).
            /// </summary>
            SWP_NOZORDER = 0x0004,

            /// <summary>
            ///     Displays the window.
            /// </summary>
            SWP_SHOWWINDOW = 0x0040,
        }
        public struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }
    }
}
