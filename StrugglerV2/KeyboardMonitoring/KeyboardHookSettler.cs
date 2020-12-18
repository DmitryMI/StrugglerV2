using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StrugglerV2.KeyboardMonitoring
{
    public static class KeyboardHookSettler
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);


        public static bool Register(IntPtr hWnd, int id, Keys key, KeyModifiers modifiers)
        {
            int vkCode = (int) key;
            int modifiersCode = (int) modifiers;
            return RegisterHotKey(hWnd, id, modifiersCode, vkCode);
        }

        public static bool Unregister(IntPtr hWnd, int id)
        {
            return UnregisterHotKey(hWnd, id);
        }
    }
}
