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

namespace PL;

/// <summary>
/// Interaction logic for OrderView.xaml
/// </summary>
public partial class OrderWindow : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get();
    BO.Cart myCart = new();
    public OrderWindow(int id, BO.Cart cart, BlApi.IBl? b)
    {
        InitializeComponent();
        bl = b;//new bl
        myCart = cart;
        BO.Order ord = new();
        try
        {
            ord = bl?.order.ReadBoOrder(id)!;//get the order from BO with matching id
        }
        catch (BO.BOEntityDoesNotExistException ex)
        {
            new ErrorWindow("Order View Window\n", ex.Message).ShowDialog();
        }
        catch (BO.Exceptions ex)
        {
            new ErrorWindow("Order View Window\n", ex.Message).ShowDialog();
        }
        DataContext = ord;
        ProductItemGrid.DataContext = ord.Items;

    }

    void clickBackBtn(object sender, RoutedEventArgs e)
    {
        Close();//close this window
    }
}
