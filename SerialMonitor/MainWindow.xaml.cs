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
        //DeviceSerial arduinoSerial;


        public MainWindow()
        {
            InitializeComponent();
            //arduinoSerial = new DeviceSerial();

  
            

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // arduinoSerial.Connect(3);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //arduinoSerial.Close();

        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
