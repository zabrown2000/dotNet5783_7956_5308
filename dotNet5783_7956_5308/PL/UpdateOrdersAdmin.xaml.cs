using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for UpdateOrdersAdmin.xaml
    /// </summary>
    public partial class UpdateOrdersAdmin : Window
    {

        BlApi.IBl? bl = BlApi.Factory.Get();
        private BO.OrderForList o = new();
        public UpdateOrdersAdmin(BlApi.IBl? b)
        {
            InitializeComponent();
            bl = b;//new bl
            DataContext = o;
            updateShip.Visibility = Visibility.Collapsed;//update invisible  
            updateDelivery.Visibility = Visibility.Collapsed;//update invisible 

        }
        public UpdateOrdersAdmin(BO.OrderForList orderForList, BlApi.IBl? b)
        {
            InitializeComponent();
            bl = b;//new bl
            o = orderForList;
            DataContext = o;
            updateShip.Visibility = Visibility.Visible;//show update
            updateDelivery.Visibility = Visibility.Visible;//show update
            ID.IsReadOnly = true;//cant change id in update 
            tname.IsReadOnly = true;
            tinstock.IsReadOnly = true;
            tprice.IsReadOnly = true;
            status.IsReadOnly = true;
        }

        void clickBackBtn(object sender, RoutedEventArgs e)
        {
            new AdminScreen().ShowDialog();
            Close();//close this window
        }


        private void updateShip_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                bl!.order.ShipUpdate(o.ID);

            }
            catch (DO.IDDoesNotExistException ex)
            {
                new ErrorWindow("Update Orders For Admin\n", ex.Message).ShowDialog();

            }

        Close();
        }

        private void updateDelivery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl!.order.DeliveredUpdate(o.ID);
            }
            catch (DO.IDDoesNotExistException ex)
            {
                new ErrorWindow("Update Orders For Admin\n", ex.Message).ShowDialog();
            }
            Close();

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new AdminScreen().ShowDialog();
            Close();
        }
    }
}

