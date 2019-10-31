using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Lab6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public double BarOpenTime { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();

            //skapa bouncer och logsystem stoppa in log ststem i bouncern.
        }
        private void RefreshListboxes()
        {
            bartenderEventListBox.Items.Refresh();
            waitressEventListBox.Items.Refresh();
            patronsEventListBox.Items.Refresh();
        }

        void UIOnBarOpen()
        {
            OpenOrCloseThePub.Content = "Close the bar";
            SliderValueTextBox.IsEnabled = false;
            sliderValue.IsEnabled = false;
        }

        void UIOnBarClosed()
        {
            OpenOrCloseThePub.Content = "Open the bar";
            SliderValueTextBox.IsEnabled = true;
            sliderValue.IsEnabled = true;
        }
    }
}
