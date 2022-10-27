
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

        public RelayCommand OpenButtonCommand { get; }
        public RelayCommand CloseButtonCommand { get; }

        
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
            //Connect(1);

            OpenButtonCommand = new RelayCommand(ConnectButton);
            CloseButtonCommand = new RelayCommand(CloseButton);
        }

        private void ConnectButton()
        {
            Connect(8);
        }
        private void CloseButton()
        {
            Close();
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
                var values = Convert.ToDouble(ReceiveData);
                SeriesCollection[0].Values.Add(values);
                //Debug.WriteLine(ReceiveData);
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
