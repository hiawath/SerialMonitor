using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialMonitor
{
    
    public class MainWindowViewModel:ObservableObject
    {
        public SeriesCollection SeriesCollection { get; set; }

        readonly IDeviceSerial deviceSerial = new DummySerial();//();DeviceSerial

        private string receiveData = "";
        public string ReceiveData
        {
            get => receiveData;
            set => SetProperty(ref receiveData, value);



        }

        public RelayCommand OpenButtonCommand { get; }
        public RelayCommand CloseButtonCommand { get; }

        public MainWindowViewModel()
        {
            deviceSerial.DataEvent += new EventHandler(DataReceived);
            OpenButtonCommand = new RelayCommand(ConnectButton);
            CloseButtonCommand = new RelayCommand(CloseButton);
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
        }

        private void DataReceived(object? sender, EventArgs e)
        {
            var temp = (IDeviceSerial)sender;
            ReceiveData=temp.ReceiveData;

            var values = Convert.ToDouble(ReceiveData);
            SeriesCollection[0].Values.Add(values);
        }

        private void ConnectButton()
        {
            deviceSerial.Connect(8);
        }
        private void CloseButton()
        {
            deviceSerial.Close();
        }
    }
}
