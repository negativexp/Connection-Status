using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Conneciton_status_WPF_
{
    class KeyboardHook
    {
        static MainWindow mainwindow = new MainWindow();
        static SettingsWindow settingswindow = new SettingsWindow();
        //DLL Import
        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc callback, IntPtr hInstance, uint threadId);

        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        [DllImport("user32.dll")]
        static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static LowLevelKeyboardProc callbackDelegate;

        const int WH_KEYBOARD_LL = 13;
        const int WM_KEYDOWN = 0x100;

        private LowLevelKeyboardProc _proc = HookProc;

        private static IntPtr hhook = IntPtr.Zero;

        public void SetHook()
        {
            //fixed
            IntPtr hInstance = LoadLibrary("User32");
            callbackDelegate = new LowLevelKeyboardProc(HookProc);
            hhook = SetWindowsHookEx(WH_KEYBOARD_LL, callbackDelegate, hInstance, 0);
        }

        public static void UnHook()
        {
            UnhookWindowsHookEx(hhook);
        }

        static bool LModifierPressed = false;
        static bool RModifierPressed = false;
        public static bool Pressed = false;
        public static bool Pressed2 = false;

        public static IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                if (vkCode == 160 || vkCode == 161) // left and right Shift
                {
                    if(vkCode == 160)
                    {
                        LModifierPressed = true;
                    }
                    else if(vkCode == 161)
                    {
                        RModifierPressed = true;
                    }

                }
                else if (RModifierPressed == true && vkCode == 119) // RShift + F8
                {
                    RModifierPressed = false;
                    if (!Pressed2)
                    {
                        Pressed2 = true;
                        settingswindow.Show();
                    }
                    else
                    {
                        Pressed2 = false;
                        settingswindow.Hide();
                    }
                }
                else if (LModifierPressed == true && vkCode == 119) // LShift + F8
                {
                    LModifierPressed = false;
                    App.Current.Shutdown();
                }
                else if (vkCode == 119) // F8
                {
                    if (!Pressed)
                    {
                        Pressed = true;
                        App.Current.MainWindow.Hide();
                    }
                    else
                    {
                        Pressed = false;
                        App.Current.MainWindow.Show();
                    }
                }
                else
                {
                    RModifierPressed = false;
                    LModifierPressed = false;
                }

                // return (IntPtr)1; // note: this will interfere with keyboard processing for other apps
            }
            return CallNextHookEx(hhook, code, (int)wParam, lParam);
        }
    }
}
