using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StrugglerV2.StrugglerActing
{
    public class PostMessageActuator : StrugglerActuator
    {
        private Process _process;
       

        const UInt32 WmKeyDown = 0x0100;
        private const UInt32 WmKeyUp = 0x0101;

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        protected override void SimulateKeyDown()
        {
            Keys key = TargetButton;
            int wParam = (int)key;
            PostMessage(_process.MainWindowHandle, WmKeyDown, wParam, 0);
        }

        protected override void SimulateKeyUp()
        {
            Keys key = TargetButton;
            int wParam = (int)key;
            PostMessage(_process.MainWindowHandle, WmKeyUp, wParam, 0);
        }

        public PostMessageActuator(Keys targetButton, int periodOuterMs, int periodInnerMs, Process process) : base(targetButton, periodOuterMs, periodInnerMs)
        {
            _process = process;
        }
    }
}
