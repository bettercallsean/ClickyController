using ClickyControllerGUI.ViewModels;
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
using System.Windows.Threading;

namespace ClickyControllerGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(10)
            };
            timer.Tick += GetMouseCoordinates;
            timer.Start();

        }

        public void GetMouseCoordinates(object sender, EventArgs e)
        {
            XCoordinates.Text = string.Format("{0}", ClickyController.Mouse.XCoordinate);
            YCoordinates.Text = string.Format("{0}", ClickyController.Mouse.YCoordinate);
        }
    }
}
