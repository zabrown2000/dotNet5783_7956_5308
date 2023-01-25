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
using System.Collections.ObjectModel;
using BO;
using System.ComponentModel;

namespace PL;

/// <summary>
/// Interaction logic for AdminScreen.xaml
/// </summary>
public partial class AdminScreen : Window
{
    ObservableCollection<BO.ProductForList?> productsForList = new();
    ObservableCollection<BO.OrderForList?> ordersForList = new();
    private BlApi.IBl? bl = BlApi.Factory.Get();
    public AdminScreen()
    {
        InitializeComponent();
        productsForList.Clear();
        ordersForList.Clear();
        try
        {
            productsForList = PL.Extensions.ToObservableCollection((bl?.products.ReadProductsForList()!)); 
            ordersForList = PL.Extensions.ToObservableCollection(bl!.order.ReadAllOrderForList()!);
        }
        catch (BO.Exceptions ex)//id is null error on screen
        {
            new ErrorWindow("List View Window\n", ex.Message).ShowDialog();
        }
        catch (BO.BOEntityDoesNotExistException ex)
        {
            new ErrorWindow("List View Window\n", ex.Message).ShowDialog();
        }
        catch (BO.InvalidInputException ex)
        {
            new ErrorWindow("List View Window\n", ex.Message).ShowDialog();
        }
        AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProdCategory));
        ProductItemGrid.DataContext = productsForList;
        ItemGrid.DataContext = ordersForList;
    }



    private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        BO.Enums.ProdCategory c = (BO.Enums.ProdCategory)AttributeSelector.SelectedItem;//save the category picked

        if (c == BO.Enums.ProdCategory.None)//if selected to view all products 
        {
            try
            {

                productsForList = PL.Extensions.ToObservableCollection((bl?.products.ReadProductsForList()!)); //get catalog products from BO
            }
            catch (BO.Exceptions ex)
            {
                new ErrorWindow("List View Window\n", ex.Message).ShowDialog();
            }
            AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProdCategory));//show all of combobox options
            ProductItemGrid.DataContext = productsForList;

            return;
        }
        if (c is BO.Enums.ProdCategory ca)
        {
            try
            {
                productsForList = PL.Extensions.ToObservableCollection(from p in bl?.products.ReadProductsForList()//get all products
                                                                   where p.Category == c
                                                                   select p);//show filtered list
            }
            catch (BO.Exceptions ex)
            {
                new ErrorWindow("List View Window\n", ex.Message).ShowDialog();
            }
            ProductItemGrid.DataContext = productsForList;

        }

        try
        {
            productsForList = PL.Extensions.ToObservableCollection(from p in bl?.products.ReadProductsForList()//get all products
                                                               where p.Category == c
                                                               select p);
        }
        catch (BO.Exceptions ex)
        {
            new ErrorWindow("List View Window\n", ex.Message).ShowDialog();
        }
        ProductItemGrid.DataContext = productsForList;

    }


    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        new ProductWindow().ShowDialog();
        try
        {
            productsForList = PL.Extensions.ToObservableCollection(bl?.products.ReadProductsForList()!);
        }
        catch (BO.Exceptions ex)
        {
            new ErrorWindow("List View Window\n", ex.Message).ShowDialog();
        }
        ProductItemGrid.DataContext = productsForList;
    }

    private void ProductItemGrid_updates(object sender, MouseButtonEventArgs e)
    {

        //ProductsListView.ItemsSource = bl?.products.ReadProductsForList(); // update list view after add

        if (ProductItemGrid.SelectedItem is BO.ProductForList productForList)
        {
            //new ProductWindow(productForList, bl).ShowDialog();
            
            BO.Products prod = new BO.Products();
            prod = bl!.products.ManagerProduct(productForList.ID);
            new ProductWindow(prod).ShowDialog();
        }
        try
        {
            productsForList = PL.Extensions.ToObservableCollection(bl?.products.ReadProductsForList()!);
        }
        catch (BO.Exceptions ex)
        {
            new ErrorWindow("List View Window\n", ex.Message).ShowDialog();
            //id is null error on screen
        }
        ProductItemGrid.DataContext = productsForList;
    }
    private void Orders_updates(object sender, MouseButtonEventArgs e)
    {
        if (ItemGrid.SelectedItem is BO.OrderForList orderForList)
        {
            new UpdateOrdersAdmin(orderForList, bl!).ShowDialog();
        }
        try
        {
            ordersForList = PL.Extensions.ToObservableCollection(bl?.order.ReadAllOrderForList()!);
        }
        catch (BO.Exceptions ex)
        {
            new ErrorWindow("List View Window\n", ex.Message).ShowDialog();
            //id is null error on screen
        }
        ItemGrid.DataContext = ordersForList;
    }

    void clickOnHomeBtn(object sender, RoutedEventArgs e)
    {
        new OpeningWindow().ShowDialog();
        Close();//close this window
    }
    private void GroupByStatus_Click(object sender, RoutedEventArgs e)
    {
        RemoveGroupings_Click(sender, e);//remove prev grouping
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ItemGrid.ItemsSource);
        PropertyGroupDescription groupDescription = new PropertyGroupDescription("Status");
        SortDescription sortDscription = new SortDescription("Status", ListSortDirection.Ascending);
        view.GroupDescriptions.Add(groupDescription);
        view.SortDescriptions.Add(sortDscription);
        Group.IsEnabled = false;
    }

    private void RemoveGroupings_Click(object sender, RoutedEventArgs e)
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ordersForList);
        view.GroupDescriptions.Clear();
        view.SortDescriptions.Clear();
        UndoGroup.IsEnabled = false;
    }
}

