using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SerialMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DeviceSerial arduinoSerial;


        DispatcherTimer dispatcher=new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            arduinoSerial = new DeviceSerial();

            dispatcher.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcher.Interval=new TimeSpan(0, 0, 0,0,100);
            dispatcher.Start();

            

        }

        private void dispatcherTimer_Tick(object? sender, EventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            arduinoSerial.Connect(7);
            dispatcher.Start();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            arduinoSerial.Close();
            dispatcher.Stop();
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
