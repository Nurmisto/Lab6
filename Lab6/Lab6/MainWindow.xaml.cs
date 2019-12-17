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
        private BarController barController;
        public MainWindow()
        {
            InitializeComponent();
            barController = new BarController(this);
            UIOnBarClosed();
        }

        public void UIOnBarOpen()
        {
            OpenOrCloseThePub.Content = "Close the bar";
         
        }

        public void UIOnBarClosed()
        {
            OpenOrCloseThePub.Content = "Open the bar";
       
        }

        
    }
}
