using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BlApi;
using BlImplementation;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window // changed from Window
    {
        private IBl bl = new Bl();
        public ProductListWindow()
        {
            InitializeComponent();
            ProductsListView.ItemsSource = bl.products.ReadProductsForList();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProdCategory));
        }
        private void AddButton_Click(object sender, RoutedEventArgs e) => new ProductWindow().Show();

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Enums.ProdCategory productCategory = (BO.Enums.ProdCategory)CategorySelector.SelectedItem; // saves the selected category
            if (productCategory == BO.Enums.ProdCategory.None) // if the user would like to view all the products
            {
                ProductsListView.ItemsSource = bl.products.ReadProductsForList();
                CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProdCategory));
                return;
            }
            if (productCategory is BO.Enums.ProdCategory cat)
            {
                //show the filtered list
                ProductsListView.ItemsSource = bl?.products?.ReadProductsForList()?.Select(x => x.Category == cat);


            }
            ProductsListView.ItemsSource = from product in bl?.products.ReadProductsForList()
                                           where product.Category == productCategory
                                           select product;
        }

        private void ProductsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DoubleClickEvent(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ProductsListView.SelectedItem is BO.ProductForList productForList)
            {
                BO.Products prod = new BO.Products();
                prod = bl.products.ManagerProduct(productForList.ID);
                new ProductWindow(prod).ShowDialog();
            }
            ProductsListView.ItemsSource = bl?.products.ReadProductsForList(); // update list view after add
        }

    }
}


