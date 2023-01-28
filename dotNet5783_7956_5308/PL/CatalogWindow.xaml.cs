using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
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
/// Interaction logic for CatalogWindow.xaml
/// </summary>
public partial class CatalogWindow : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get();
    ObservableCollection<BO.ProductItem> catalog = new();
    BO.Products product = new BO.Products();
    BO.Cart cart = new();

    public CatalogWindow()
    {
        InitializeComponent();
        catalog.Clear();
        try
        {
            catalog = Extensions.ToObservableCollection(bl!.products.GetCatalog())!;
        }
        catch (BO.BOEntityDoesNotExistException exc)
        {
            MessageBox.Show(exc.Message, "Catalog Window", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catalogGrid.DataContext = catalog;
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProdCategory));
    }

    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        BO.Enums.ProdCategory productCategory = (BO.Enums.ProdCategory)CategorySelector.SelectedItem; // saves the selected category
        if (productCategory == BO.Enums.ProdCategory.None || CategorySelector.SelectedItem == null) // if the user would like to view all the products
        {
            try
            {
                catalog = Extensions.ToObservableCollection(bl!.products.GetCatalog())!;//get catalog products from BO
            }
            catch (BO.BOEntityDoesNotExistException exc)
            {
                MessageBox.Show(exc.Message, "Catalog Window", MessageBoxButton.OK, MessageBoxImage.Error);
                //new ErrorWindow("Catalog Window\n", exc.Message).ShowDialog();
            }
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProdCategory));
            catalogGrid.DataContext = catalog;
            return;
        }
        if (productCategory is BO.Enums.ProdCategory cat)
        {
            try
            {
                //show the filtered list
                catalog = Extensions.ToObservableCollection(from p in bl?.products.GetCatalog()//get all products
                                                           where p.Category == cat
                                                           select p);
            }
            catch (BO.BOEntityDoesNotExistException exc)
            {
                MessageBox.Show(exc.Message, "Catalog Window", MessageBoxButton.OK, MessageBoxImage.Error);
                //new ErrorWindow("Catalog Window\n", exc.Message).ShowDialog();
            }

        }
        catalogGrid.DataContext = catalog;
    }

    private void ProductItemView_click(object sender, MouseButtonEventArgs e)
    {
        if (catalogGrid.SelectedItem is BO.ProductItem productItem)
        {
            BO.Products prod = new BO.Products();
            prod = bl?.products.ManagerProduct(productItem.ID)!;
            new ProductWindow(prod).ShowDialog(); //another param, "overload" for the product window, why?
        }
        try
        {
            catalog = Extensions.ToObservableCollection(bl!.products.GetCatalog())!;
        }
        catch (BO.BOEntityDoesNotExistException exc)
        {
            MessageBox.Show(exc.Message, "List View Window", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catalogGrid.DataContext = catalog;
    }

    private void ProductsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void AddToCart_Click(object sender, RoutedEventArgs e)
    {
        if (catalogGrid.SelectedItem is BO.ProductItem productItem)
        {
            try
            {
                cart = (bl!.cart.AddToCart(cart, productItem.ID)); //add the selected product to cart
                MessageBox.Show("Product successfully added to your cart.");
            }
            catch (BO.BOEntityDoesNotExistException exc)
            {
                MessageBox.Show(exc.Message, "Catalog Window", MessageBoxButton.OK, MessageBoxImage.Error);
                //new ErrorWindow("Cart Window Window", ex.Message).ShowDialog();
            }
            catch (BO.OutOfStockException exc)
            {
                MessageBox.Show(exc.Message, "Catalog Window", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void ViewCart_Click(object sender, RoutedEventArgs e)
    {
        new CartWindow(cart).Show();
        Close();
    }

    private void ReturnHome_Click(object sender, RoutedEventArgs e)
    {
        new OpeningWindow().Show();
        Close();
    }

    private void GroupByCategory_Click(object sender, RoutedEventArgs e)
    {
        RemoveGroupings_Click(sender, e);//remove prev grouping
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(catalogGrid.ItemsSource);
        PropertyGroupDescription groupDescription = new PropertyGroupDescription("Category");
        SortDescription sortDscription = new SortDescription("Category", ListSortDirection.Ascending);
        view.GroupDescriptions.Add(groupDescription);
        view.SortDescriptions.Add(sortDscription);
        GroupByCategory.IsEnabled = false; // used to say GroupByStatus
    }

    private void RemoveGroupings_Click(object sender, RoutedEventArgs e)
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(catalog);
        view.GroupDescriptions.Clear();
        view.SortDescriptions.Clear();
        GroupBack.IsEnabled = false;

    }
}
