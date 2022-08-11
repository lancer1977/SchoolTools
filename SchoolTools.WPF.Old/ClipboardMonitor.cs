﻿using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Markup;

namespace PolyhydraGames.Code
{
    public class ClipboardArgs : EventArgs
    {
        public string Text;

        public ClipboardArgs(string data)
        {
            Text = data;
        }
    }

    public abstract class ClipboardWindow : Window
    {
        public delegate void ReadClipboardHandler(object sender, ClipboardArgs args);
        public event ReadClipboardHandler MonitorClipboard;
        private void NotifyClipboardChanged(string text)
        {
            if (MonitorClipboard == null) return;
            var args = new ClipboardArgs(text);
            MonitorClipboard(null, args);
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            if (hwndSource != null)
            {
                _installedHandle = hwndSource.Handle;
                _viewerHandle = SetClipboardViewer(_installedHandle);
                hwndSource.AddHook(HwndSourceHook);
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            ChangeClipboardChain(_installedHandle, _viewerHandle);
            int error = Marshal.GetLastWin32Error();
            e.Cancel = error != 0;

            base.OnClosing(e);
        }
        protected override void OnClosed(EventArgs e)
        {
            _viewerHandle = IntPtr.Zero;
            _installedHandle = IntPtr.Zero;
            base.OnClosed(e);
        }
        private IntPtr HwndSourceHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            try
            {
                switch (msg)
                {
                    case WmChangecbchain:
                        _viewerHandle = lParam;
                        if (_viewerHandle != IntPtr.Zero)
                        {
                            SendMessage(_viewerHandle, msg, wParam, lParam);
                        }

                        break;

                    case WmDrawclipboard:
                        if (_viewerHandle != IntPtr.Zero)
                        {
                            SendMessage(_viewerHandle, msg, wParam, lParam);
                        }
                        OnClipboardChanged();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return IntPtr.Zero;
        }

        private void OnClipboardChanged()
        {
            var text = Clipboard.GetText();
            if (String.IsNullOrEmpty(text)) return;
            NotifyClipboardChanged(text);
        }
        private const int WmDrawclipboard = 0x308;
        private const int WmChangecbchain = 0x30D;
        private IntPtr _installedHandle = IntPtr.Zero;
        private IntPtr _viewerHandle = IntPtr.Zero;

        [DllImport("user32.dll")]
        private static extern IntPtr SetClipboardViewer(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ChangeClipboardChain(IntPtr hWnd, IntPtr hWndNext);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);


    }

    public enum ClipboardFormat : byte
    {
        /// <summary>
        ///     Specifies the standard ANSI text format. This static field is read-only.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        Text,

        /// <summary>
        ///     Specifies the standard Windows Unicode text format. This static field
        ///     is read-only.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        UnicodeText,

        /// <summary>
        ///     Specifies the Windows device-independent bitmap (DIB) format. This static
        ///     field is read-only.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        Dib,

        /// <summary>Specifies a Windows bitmap format. This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        Bitmap,

        /// <summary>
        ///     Specifies the Windows enhanced metafile format. This static field is
        ///     read-only.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        EnhancedMetafile,

        /// <summary>
        ///     Specifies the Windows metafile format, which Windows Forms does not
        ///     directly use. This static field is read-only.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        MetafilePict,

        /// <summary>
        ///     Specifies the Windows symbolic link format, which Windows Forms does
        ///     not directly use. This static field is read-only.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        SymbolicLink,

        /// <summary>
        ///     Specifies the Windows Data Interchange Format (DIF), which Windows Forms
        ///     does not directly use. This static field is read-only.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        Dif,

        /// <summary>
        ///     Specifies the Tagged Image File Format (TIFF), which Windows Forms does
        ///     not directly use. This static field is read-only.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        Tiff,
        OemText,
        Palette,
        PenData,
        Riff,
        WaveAudio,
        FileDrop,
        Locale,
        Html,
        Rtf,
        CommaSeparatedValue,
        StringFormat,
        Serializable,
    }
}