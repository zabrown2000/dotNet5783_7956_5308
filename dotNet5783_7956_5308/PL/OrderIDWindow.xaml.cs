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

namespace PL;

/// <summary>
/// Interaction logic for OrderIDWindow.xaml
/// </summary>
public partial class OrderIDWindow : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get();
    BO.Cart myCart = new();
    public OrderIDWindow(BO.Cart cart, BlApi.IBl b)
    {
        InitializeComponent();
        bl = b;//new bl
        cart = myCart;
        orderId.Text = "";
    }

    private void OKBtn_Click(object sender, RoutedEventArgs e)
    {
        int id = 0;
        try
        {
            id = int.Parse(orderId.Text);//save the entered id as a number
        }
        catch (System.FormatException)
        {
            new ErrorWindow("Enter Order ID Window", "Wrong id number entered").ShowDialog();
        }
        new OrderTracking(id, myCart, bl!).ShowDialog();//open order tracking window with entered id
        this.Close(); //close current window
    }
    private void tid_previewtextinput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);//only gets numbers for id
    }

    void clickBackBtn(object sender, RoutedEventArgs e)
    {
        new OpeningWindow().ShowDialog();
        Close(); //close this window
    }
    private void EnterPressed_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter) OKBtn_Click(sender, e);
    }
}
