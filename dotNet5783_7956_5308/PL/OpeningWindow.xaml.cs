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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void OrderIDWindow_Click(object sender, RoutedEventArgs e)
        {
            new OrderIDWindow().ShowDialog();
            Close();//close this window
        }

        private void openCatalog_Click(object sender, RoutedEventArgs e)
        {
            new CatalogWindow().ShowDialog();
            Close();//close this window
        }
    }
}
