﻿using BO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    BO.OrderTracking orderTracking = new();
    BO.Cart myCart = new();
    public OrderTracking(BO.Cart cart, BlApi.IBl? b)//empty ctor
    {
        InitializeComponent();
        bl = b;//new bl
        myCart = cart;
        DataContext = orderTracking;
    }
    public OrderTracking(int id, BO.Cart cart, BlApi.IBl? b)
    {
        InitializeComponent();
        bl = b;//new bl
        myCart = cart;
        BO.OrderTracking o = new();
        try
        {
            o = bl?.order.GetOrderTracking(id)!;
        }
        catch (BO.BOEntityDoesNotExistException ex)
        {
            new ErrorWindow("Order Tracking Window\n", ex.Message).ShowDialog();
        }
        //orderTracking = PL.Tools.CastBoOTToPo(o);//get matching po order tracking
        DataContext = orderTracking;//set data context

    }

    private void OrderDetails_Click(object sender, RoutedEventArgs e)
    {
        new OrderWindow(orderTracking.Id, myCart, bl!).ShowDialog();
        Close();//close this window
    }
    void clickBackBtn(object sender, RoutedEventArgs e)
    {
        new OrderIDWindow(myCart, bl!).ShowDialog();
        Close();//close this window
    }
    private void HomeBtn_Click(object sender, RoutedEventArgs e)
    {
        new MainWindow().ShowDialog();//go to home window 
    }
}