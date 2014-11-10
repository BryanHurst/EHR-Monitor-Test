// Default C# Class Imports
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Additional Imports used by the EHR Monitor Test
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace EHR_Monitor_Test
{
    public class EHRText
    {
        [DllImport("user32.dll", EntryPoint="GetForegroundWindow")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", EntryPoint="GetWindowThreadProcessId", SetLastError=true)]
        private static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out IntPtr lpdwProcessId);

        [DllImport("user32.dll", EntryPoint="GetGUIThreadInfo", SetLastError=true)]
        private static extern bool GetGUIThreadInfo(IntPtr hThreadID, ref GUITHREADINFO lpgui);

        [DllImport("user32.dll", EntryPoint="SendMessage", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, [Out] StringBuilder lParam);

        [DllImport("user32.dll", EntryPoint="SendMessage", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, int wParam, [Out] string lParam);

        private struct RECT
        {
            public int iLeft;
            public int iTop;
            public int iRight;
            public int iBottom;
        }

        private struct GUITHREADINFO
        {
            public int cbSize;
            public int flags;
            public IntPtr hwndActive;
            public IntPtr hwndFocus;
            public IntPtr hwndCapture;
            public IntPtr hwndMenuOwner;
            public IntPtr hwndMoveSize;
            public IntPtr hwndCaret;
            public RECT rectCaret;
        }

        private static void SetText(IntPtr handle, string text)
        {
            const uint WM_SETTEXT = 0x000C;

            SendMessage(handle, WM_SETTEXT, 0, text);
        }

        // Gets text text from a control by it's handle.
        private static string GetText(IntPtr handle)
        {
            const uint WM_GETTEXTLENGTH = 0x000E;
            const uint WM_GETTEXT = 0x000D;

            // Gets the text length.
            var length = (int)SendMessage(handle, WM_GETTEXTLENGTH, IntPtr.Zero, null);

            // Init the string builder to hold the text.
            var sb = new StringBuilder(length + 1);

            // Writes the text from the handle into the StringBuilder
            SendMessage(handle, WM_GETTEXT, (IntPtr)sb.Capacity, sb);

            // Return the text as a string.
            return sb.ToString();
        }

        public static string GetTextFromActiveWindowElement()
        {
            try
            {
                IntPtr windowHWnd = GetForegroundWindow();
                IntPtr lpdwProcessId;
                IntPtr threadId = GetWindowThreadProcessId(windowHWnd, out lpdwProcessId);

                GUITHREADINFO lpgui = new GUITHREADINFO();
                lpgui.cbSize = Marshal.SizeOf(lpgui);

                GetGUIThreadInfo(threadId, ref lpgui);

                return GetText(lpgui.hwndFocus);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }

            return string.Empty;
        }

        public static void SetTextOfActiveWindowElement(string text)
        {
            try
            {
                IntPtr windowHWnd = GetForegroundWindow();
                IntPtr lpdwProcessId;
                IntPtr threadId = GetWindowThreadProcessId(windowHWnd, out lpdwProcessId);

                GUITHREADINFO lpgui = new GUITHREADINFO();
                lpgui.cbSize = Marshal.SizeOf(lpgui);

                GetGUIThreadInfo(threadId, ref lpgui);

                SetText(lpgui.hwndFocus, text);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }
    }
}
