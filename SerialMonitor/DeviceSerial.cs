
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;

namespace SerialMonitor
{
    partial class DeviceSerial : IDeviceSerial
    {

        public event EventHandler DataEvent;
        public string ReceiveData { get; private set; }

        readonly SerialPort serialPort = new SerialPort();
        public DeviceSerial()
        {

        }

        public bool Connect(int portName, int baudRate = (int)9600, int DataBits = (int)8, Parity parity = Parity.None, StopBits stopBits = StopBits.One)
        {
            if (serialPort.IsOpen==false)
            {
                serialPort.PortName="COM" + portName.ToString();
                serialPort.BaudRate = baudRate;
                serialPort.Parity = parity;
                serialPort.StopBits = stopBits;
                serialPort.DataBits = DataBits;


                serialPort.Open();
                serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceiveHandler);

            }


            return true;
        }
        private void DataReceiveHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort serialPort = (SerialPort)sender;
            string temp = serialPort.ReadLine();

            if (String.IsNullOrEmpty(temp) == false)
            {
                ReceiveData = temp.Trim().ToString();
                Debug.WriteLine(ReceiveData);
                DataEvent(this, EventArgs.Empty);
            }
        }
        public bool Close()
        {
            serialPort.DataReceived -= new SerialDataReceivedEventHandler(DataReceiveHandler);

            serialPort.Close();

            return true;
        }
    }
}
