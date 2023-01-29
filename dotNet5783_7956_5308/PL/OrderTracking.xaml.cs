using BO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
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
/// Interaction logic for OrderTracking.xaml
/// </summary>
public partial class OrderTracking : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get();
    BO.OrderTracking ordTracking = new();
    BO.Cart myCart = new();

    public OrderTracking(BO.OrderTracking orderTracking)
    {
        InitializeComponent();
        bl = BlApi.Factory.Get();
        BO.OrderTracking track = new();
        try
        {
            track = bl!.order.GetOrderTracking(orderTracking.ID);
        }
        catch (BO.BOEntityDoesNotExistException exc)
        {
            MessageBox.Show(exc.Message, "Track Order Window", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        DataContext = track;
        ordTracking = track;
    }

    private void OrderDetails_Click(object sender, RoutedEventArgs e)
    {
        new OrderWindow(ordTracking.ID, myCart, bl!).ShowDialog();
        //Close();//close this window
    }

    private void ReturnHome_Click(object sender, RoutedEventArgs e)
    {
        new OpeningWindow().Show();
        Close();
    }
}