

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using ClipboardMonitor.Core;

namespace PolyhydraGames.SchoolTools.WPF.Views
{
    public  class ClipboardHandler :  IClipboardMonitor
    {
        private IntPtr windowHandle;
        public ClipboardHandler(Window window, bool start = true)
        {
            windowHandle = new WindowInteropHelper(window).EnsureHandle();
            HwndSource.FromHwnd(windowHandle)?.AddHook(HwndHandler);
            if (start) Start();
        }
        public bool MonitorClipboard { get; set; } 
        public event EventHandler<ClipboardArgs> OnClipboardChanged;
         
        private void NotifyClipboardChanged(string text)
        {
            if (MonitorClipboard == false) return;
            OnClipboardChanged?.Invoke(null, new ClipboardArgs(text));
        }
        /// <summary>
        /// Enable clipboard notification.
        /// </summary>
        public void Start()
        {
            NativeMethods.AddClipboardFormatListener(windowHandle);
        }

        public void Stop()
        {
            NativeMethods.RemoveClipboardFormatListener(windowHandle);
        }

        private IntPtr HwndHandler(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
        {
            if (msg == WM_CLIPBOARDUPDATE)
            {
                try
                {
                    var text = Clipboard.GetText();
                    if (!string.IsNullOrEmpty(text))
                    {
                        NotifyClipboardChanged(text);
                    } 
                
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
          
            }
            handled = false;
            return IntPtr.Zero;
        }
 
        private const int WM_CLIPBOARDUPDATE = 0x031D;

        private static class NativeMethods
        {
            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool AddClipboardFormatListener(IntPtr hwnd);

            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool RemoveClipboardFormatListener(IntPtr hwnd);
        }
    }
}