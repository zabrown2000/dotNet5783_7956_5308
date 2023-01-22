using BO;
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
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderView : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        PO.Order o = new();
        PO.Cart myCart = new();
        public OrderView(int id, PO.Cart cart, BlApi.IBl? b)
        {
            InitializeComponent();
            bl = b;//new bl
            myCart = cart;
            BO.Order ord = new();
            try
            {
                ord = bl?.Order.GetBoOrder(id)!;//get the order from BO with matching id
            }
            catch (BO.IdNotExistException ex)
            {
                new ErrorWindow("Order View Window\n", ex.Message).ShowDialog();
            }
            catch (BO.Exceptions ex)
            {
                new ErrorWindow("Order View Window\n", ex.Message).ShowDialog();
            }
            o = PL.Tools.CastBoOrderToPo(ord);//convert to PO order 
            DataContext = o;
            ProductItemGrid.DataContext = o.OrderItems;

        }

        void clickBackBtn(object sender, RoutedEventArgs e)
        {
            new OrderTracking(myCart, bl!).ShowDialog();
            Close();//close this window
        }
    }
}
