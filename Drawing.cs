 using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EspCs16
{
    class Drawing
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("user32.dll")]
        private static extern IntPtr ReleaseDC(IntPtr hwnd, IntPtr dc);
        [DllImport("user32.dll")]
        private static extern IntPtr GetwindowRect(IntPtr hwnd, out RECT lpRect);
        [StructLayout(LayoutKind.Sequential)]

        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Botton;
        }
        private static Size GetControlSize(IntPtr hwnd)
        {
            RECT pRect;
            Size cSize = new Size();
            GetwindowRect(hwnd, out pRect);
            cSize.Width = pRect.Right - pRect.Left - 6;
            cSize.Height = pRect.Bottom - pRect.Top - 28;

            return cSize;
        }

        public static vold DrawnRect(IntPtr win, Pen pen, Rectangle rect)
        {
            IntPtr windowDC = GetDC(win);
            Graphics G = Graphics.FramHdc(windowDc);
            G.DrawRectangle(pen, rect);
            G.Dispose();
            ReleaseDC(win, windowDc);
        }
        public static Size GetSize(String nameprocess)
        {
            Processor process = process.GetProcessesByName(nameProcess).FirstorDefault();

            if (process == null)
            {
                console.writeLine("Não existe processo aberto");
                console.ReadLine();
                return new Size();
            }
            IntPtr win = process.Mainwindowhandle;
            Size size = GetControlSize(win);
            return Size;
        }
    }
}
