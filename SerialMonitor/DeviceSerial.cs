
using CommunityToolkit.Mvvm.ComponentModel;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;
using System.Windows.Threading;

namespace SerialMonitor
{
    partial class DeviceSerial : ObservableObject
    {

        public SeriesCollection SeriesCollection { get; set; }
        SerialPort serialPort = new SerialPort();

        private string receiveData="";
        public string ReceiveData
        {
            get => receiveData;
            set => SetProperty(ref receiveData, value);
        }

        public DeviceSerial()
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title ="Series 1",
                    Values = new ChartValues<double> { 1,2,3,4,5}
                },
                new LineSeries
                {
                    Title="Series 2",
                    Values= new ChartValues<double>{10,9,8,7,6,5,4,3,2,1}
                }
            };
            Thread.Sleep(500);
            Connect(1);
            //System.Threading.Thread.Sleep(100);

            //dispatcher.Tick += new EventHandler(dispatcherTimer_Tick);
            //dispatcher.Interval=new TimeSpan(0, 0, 0, 0, 10);
            //dispatcher.Start();


        }


        public bool Connect(int portName, int baudRate = (int)9600, int DataBits=(int)8, Parity parity=Parity.None, StopBits stopBits=StopBits.One)
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
            string temp= serialPort.ReadLine();

            if (String.IsNullOrEmpty(temp) == false)
            {
                ReceiveData = temp.Trim().ToString();
                //var values = Convert.ToDouble(ReceiveData);
                //Debug.WriteLine(ReceiveData);
            }
   

            //SeriesCollection[0].Values.Add(values);

        }

        public bool Close()
        {
            serialPort.DataReceived -= new SerialDataReceivedEventHandler(DataReceiveHandler);

            serialPort.Close();

            return true;
        }

    }
}
