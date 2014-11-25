// Default C# Class Imports
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Additional Imports used by the EHR Monitor Test
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.IO;

namespace EHR_Monitor_Test
{
    public class EHRText
    {
        private const uint WM_SETTEXT = 0x000C;
        private const uint WM_GETTEXT = 0x000D;
        private const uint WM_GETTEXTLENGTH = 0x000E;
        private const uint WM_USER = 0x0400;
        private const uint WM_PASTE = 0x0302;
        private const uint WM_COPY = 0x0301;
        private const uint EM_SETSEL = 0x00B1;
        private const uint EM_STREAMOUT = WM_USER + 74;
        private const uint SF_RTF = 2;

        [DllImport("user32.dll", EntryPoint="GetForegroundWindow")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", EntryPoint="GetWindowThreadProcessId", SetLastError=true)]
        private static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out IntPtr lpdwProcessId);

        [DllImport("user32.dll", EntryPoint="GetGUIThreadInfo", SetLastError=true)]
        private static extern bool GetGUIThreadInfo(IntPtr hThreadID, ref GUITHREADINFO lpgui);

        [DllImport("user32.dll", EntryPoint="SendMessage", CharSet=CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, [Out] StringBuilder lParam);

        [DllImport("user32.dll", EntryPoint="SendMessage", CharSet=CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, uint wParam, [Out] string lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, uint wParam, ref EDITSTREAM lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, uint wParam, uint lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, uint wParam, int lParam);

        private delegate int EditStreamCallback(MemoryStream dwCookie, IntPtr pbBuff, int cb, out int pcb);

        private static int EditStreamProc(MemoryStream dwCookie, IntPtr pbBuff, int cb, out int pcb)
        {
            pcb = cb;
            byte[] buffer = new byte[cb];
            Marshal.Copy(pbBuff, buffer, 0, cb);
            dwCookie.Write(buffer, 0, cb);
            return 0;
        }

        [StructLayout(LayoutKind.Sequential)]
        private class EDITSTREAM
        {
            public MemoryStream dwCookie;
            public uint dwError;
            public EditStreamCallback pfnCallback;
        }

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
            SendMessage(handle, WM_SETTEXT, 0, text);
        }

        // Gets text text from a control by it's handle.
        private static string GetText(IntPtr handle)
        {
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

        public static void SetActiveWindowElementToClipboardContents()
        {
            try
            {
                IntPtr windowHWnd = GetForegroundWindow();
                IntPtr lpdwProcessId;
                IntPtr threadId = GetWindowThreadProcessId(windowHWnd, out lpdwProcessId);

                GUITHREADINFO lpgui = new GUITHREADINFO();
                lpgui.cbSize = Marshal.SizeOf(lpgui);

                GetGUIThreadInfo(threadId, ref lpgui);

                SetText(lpgui.hwndFocus, "\0"); // empty the target before pasting
                SendMessage(lpgui.hwndFocus, WM_PASTE, 0, 0);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        public static void SetClipboardContentsToActiveWindowElement()
        {
            try
            {
                IntPtr windowHWnd = GetForegroundWindow();
                IntPtr lpdwProcessId;
                IntPtr threadId = GetWindowThreadProcessId(windowHWnd, out lpdwProcessId);

                GUITHREADINFO lpgui = new GUITHREADINFO();
                lpgui.cbSize = Marshal.SizeOf(lpgui);

                GetGUIThreadInfo(threadId, ref lpgui);
                SendMessage(lpgui.hwndFocus, EM_SETSEL, 0, -1);
                SendMessage(lpgui.hwndFocus, WM_COPY, 0, 0);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        public static string GetRTFFromActiveWindowElement()
        {
            try
            {
                IntPtr windowHWnd = GetForegroundWindow();
                IntPtr lpdwProcessId;
                IntPtr threadId = GetWindowThreadProcessId(windowHWnd, out lpdwProcessId);

                GUITHREADINFO lpgui = new GUITHREADINFO();
                lpgui.cbSize = Marshal.SizeOf(lpgui);

                GetGUIThreadInfo(threadId, ref lpgui);

                string result = String.Empty;
                using (MemoryStream stream = new MemoryStream())
                {
                    EDITSTREAM editStream = new EDITSTREAM();
                    editStream.pfnCallback = new EditStreamCallback(EditStreamProc);
                    editStream.dwCookie = stream;

                    SendMessage(lpgui.hwndFocus, EM_STREAMOUT, SF_RTF, ref editStream);

                    stream.Seek(0, SeekOrigin.Begin);
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        result = reader.ReadToEnd();
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return null;
            }
        }
    }
}
