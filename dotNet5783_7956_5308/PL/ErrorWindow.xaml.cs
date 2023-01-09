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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        public ErrorWindow()
        {
            InitializeComponent();
        }
        public ErrorWindow(string title, string message)
        {
            InitializeComponent();
            ErrorTitle.Text = title;
            ErrorDetails.Text = message;
        }


        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //private void ErrorTitle_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}
    }
}
