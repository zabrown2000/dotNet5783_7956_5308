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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderIDWindow.xaml
    /// </summary>
    public partial class OrderIDWindow : Window
    {
        public OrderIDWindow()
        {
            InitializeComponent();
        }
        private void tid_previewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);//only gets numbers for id
        }

        private void keyPressed(object sender, TextCompositionEventArgs e)
        {
            //go to order tracking
        }
    }
}
