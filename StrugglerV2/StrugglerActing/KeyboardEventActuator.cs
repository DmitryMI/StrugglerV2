using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StrugglerV2.StrugglerActing
{
    public class KeyboardEventActuator : StrugglerActuator
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        // Declare some keyboard keys as constants with its respective code
        // See Virtual Code Keys: https://msdn.microsoft.com/en-us/library/dd375731(v=vs.85).aspx
        public const int KeyEventfExtendedkey = 0x0001; //Key down flag
        public const int KeyEventfKeyup = 0x0002; //Key up flag
        

        protected override void SimulateKeyDown()
        {
            // Simulate a key press event
            byte keyCode = (byte) TargetButton;
            keybd_event(keyCode, 0, KeyEventfExtendedkey, 0);
           
        }

        protected override void SimulateKeyUp()
        {
            byte keyCode = (byte)TargetButton;
            keybd_event(keyCode, 0, KeyEventfKeyup, 0);
        }

        public KeyboardEventActuator(Keys targetButton, int periodOuterMs, int periodInnerMs) : base(targetButton, periodOuterMs, periodInnerMs)
        {
        }
    }
}
