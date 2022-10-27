using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SerialMonitor
{
    internal class DummySerial : IDeviceSerial
    {
        public string ReceiveData { get; private set; }
        DispatcherTimer dispatcher = new DispatcherTimer();
        private int count = 0;
        public event EventHandler DataEvent;

        public DummySerial()
        {
            dispatcher.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcher.Interval=new TimeSpan(0, 0, 0, 0, 50);
            
        }

        public bool Close()
        {
            dispatcher.Stop();
            return true;
        }

        public bool Connect(int portName, int baudRate = 9600, int DataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.One)
        {
            dispatcher.Start();
            return true;
        }
        private void dispatcherTimer_Tick(object? sender, EventArgs e)
        {
            count++;
            ReceiveData=count.ToString();
            DataEvent(this, EventArgs.Empty);

        }
    }
}
