using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StrugglerV2.StrugglerActing
{
    public abstract class StrugglerActuator
    {
        private Thread _actuatorThread;
        private bool _actuatorLoopRunning = false;
        private bool _actuatorLoopSecured = true;

        protected StrugglerActuator(Keys targetButton, int periodOuterMs, int periodInnerMs)
        {
            TargetButton = targetButton;
            PeriodOuterMs = periodOuterMs;
            PeriodInnerMs = periodInnerMs;
        }
        
        public Keys TargetButton { get; set; }
        public int PeriodOuterMs { get; set; }
        public int PeriodInnerMs { get; set; }
        public bool IsRunning => !_actuatorLoopSecured;

        public void StartActuator()
        {
            StopActuator();
            _actuatorLoopRunning = true;
            _actuatorLoopSecured = false;
            _actuatorThread = new Thread(ActuatorLoop);
            _actuatorThread.Start();
        }

        public void StopActuator()
        {
            _actuatorLoopRunning = false;
            _actuatorThread?.Join();
            _actuatorLoopSecured = true;
        }


        protected abstract void SimulateKeyDown();
        protected abstract void SimulateKeyUp();
        private void ActuatorLoop()
        {
            while (_actuatorLoopRunning)
            {
                SimulateKeyDown();
                Thread.Sleep(PeriodInnerMs);
                SimulateKeyUp();
                Thread.Sleep(PeriodOuterMs);
            }
            // To not leave the key in DOWN state
            SimulateKeyUp();
        }
    }
}
