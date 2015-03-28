using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;
using NLog;
using ViewModel;

namespace MultiMeter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var device = new AccessDevice();
            device.NewMeasurement += dummy_NewMeasurement;

            var vm = new Main(device);
            DataContext = vm;
        }

        void dummy_NewMeasurement(object sender, NewMeasureValueEventArgs e)
        {
            var logger = LogManager.GetLogger(GetType().Name);
            logger.Log(LogLevel.Info, e.Value.ToString());
        }
    }
}
