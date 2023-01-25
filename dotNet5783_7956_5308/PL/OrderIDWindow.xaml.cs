using BO;
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
    /// Interaction logic for EnterIDWindow.xaml
    /// </summary>
    public partial class OrderIDWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public OrderIDWindow()
        {
            InitializeComponent();
            IDInput.Text = "";
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            int ID = 0;
            BO.Order order = new BO.Order();
            try
            {
                ID = int.Parse(IDInput.Text);//save the entered id as a number
            }
            catch (System.FormatException)
            {
                //MessageBox.Show("Wrong ID number entered", "Enter Order ID Window", MessageBoxButton.OK, MessageBoxImage.Error);
                new ErrorWindow("Enter Order ID Window", "Wrong ID number entered").ShowDialog();
            }
            try
            {
                order = bl!.order.ReadBoOrder(ID);
            }
            catch (BO.BOEntityDoesNotExistException exc)
            {
                MessageBox.Show(exc.Message, "Order List Window", MessageBoxButton.OK, MessageBoxImage.Error);
                //new ErrorWindow("Enter Order ID Window\n", exc.Message).ShowDialog();
                Close();
            }
            BO.OrderTracking orderTracking = new();
            orderTracking.Id = order.ID;
            orderTracking.Status = order.Status;
            Close();//close current window                    
            new OrderTracking(orderTracking).ShowDialog();
        }
        private void tid_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);//only gets numbers for id
        }

        void clickBackBtn(object sender, RoutedEventArgs e)
        {
            new MainWindow().ShowDialog();
            Close();//close this window
        }
        private void EnterPressed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) OKBtn_Click(sender, e);
        }
    }
}
