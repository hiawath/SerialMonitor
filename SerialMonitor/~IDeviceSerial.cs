using System;
using System.IO.Ports;

namespace SerialMonitor
{
    internal interface IDeviceSerial
    {
        string ReceiveData { get; }

        event EventHandler DataEvent;

        bool Close();
        bool Connect(int portName, int baudRate = 9600, int DataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.One);
    }
}