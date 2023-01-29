using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
/// Interaction logic for CartWindow.xaml
/// </summary>
public partial class CartWindow : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get();
    ObservableCollection<BO.OrderItem> items = new();
    BO.Cart cart = new BO.Cart();
    int productID;

    public CartWindow()
    {
        InitializeComponent();
    }

    public CartWindow(BO.Cart myCart)
    {
        InitializeComponent();
        items.Clear();
        cart = myCart;
   
        try
        {
            items = Extensions.ToObservableCollection(bl!.cart.GetItems(cart));
        }
        catch
        {
            MessageBox.Show("Error", "Cart Window", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        cartGrid.DataContext = items;
        TotalPrice.Text = cart.TotalPrice.ToString();
    }

    private void CheckOut_Click(object sender, RoutedEventArgs e)
    {
        new CheckOutWindow(cart).Show();
        Close();
    }
    private void ReturnHome_Click(object sender, RoutedEventArgs e)
    {
        new OpeningWindow().Show();
        Close();
    }

    private void Increase_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (cartGrid.SelectedItem is BO.OrderItem orderItem)
            {
                //myCart = PL.Tools.CastPoCToBo(poCart);
                cart = bl!.cart.IncreaseCart(cart, orderItem.ProductID);
            }
            //poCart = bl.Cart.IncreaseCart(poCart, cartGrid.SelectedItem.ID);
        }
        catch (BO.OutOfStockException exc)
        {
            MessageBox.Show(exc.Message, "Cart Window", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (BO.BOEntityDoesNotExistException exc)
        {
            MessageBox.Show(exc.Message, "Cart Window", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        //cartGrid.DataContext = items;
        
        
        new CartWindow(cart).Show();
        Close();
    }
    private void Decrease_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (cartGrid.SelectedItem is BO.OrderItem orderItem)
            {
                cart = bl!.cart.DecreaseCart(cart, orderItem.ProductID);
            }
        }
        catch (BO.BOEntityDoesNotExistException exc)
        {
            MessageBox.Show(exc.Message, "Cart Window", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        //cartGrid.DataContext = items;

        new CartWindow(cart).Show();
        Close();
    }
}
