using BlApi;
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
using BlImplementation;
using System.Diagnostics;
using System.Security.Cryptography;

namespace PL
{
    /// <summary>
    /// Interaction logic for AppendWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();
        private BO.Products product = new BO.Products();
        public ProductWindow() 
        {
            InitializeComponent();
            CategoryBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProdCategory));
            updateProductButton.Visibility = Visibility.Collapsed; //update button is invisible 
            uinstock.Visibility = Visibility.Collapsed;
            uprice.Visibility = Visibility.Collapsed;
            uname.Visibility = Visibility.Collapsed;
            product.ID = bl!.products.GetNextID();
            ID.Text = product.ID.ToString();
        }

        public ProductWindow(Products prod)
        {
            InitializeComponent();
            CategoryBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProdCategory));
            addProductButton.Visibility = Visibility.Collapsed; //add button is invisible
            updateProductButton.Visibility = Visibility.Visible;//show update
            tinstock.Visibility = Visibility.Collapsed;
            tprice.Visibility = Visibility.Collapsed;
            tname.Visibility = Visibility.Collapsed;
            //setting values for display
            uinstock.Text = prod.InStock.ToString();
            uprice.Text = prod.Price.ToString();
            CategoryBox.SelectedItem = prod.Category;
            uname.Text = prod.Name;
            ID.Text = prod.ID.ToString();
            //setting values for class variable, need for updating
            product.ID = prod.ID;
            product.InStock = prod.InStock;
            product.Price = prod.Price;
            product.Category = prod.Category;
            product.Name = prod.Name;
        }

        public ProductWindow(BO.ProductForList productForList, BlApi.IBl? b)//update ctor
        {
            InitializeComponent();
            bl = b;//new bl
            BO.Products prod = bl!.products.ManagerProduct(productForList.ID);//save the matching product or product for list
            DataContext = productForList;//set product as data context

            CategoryBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProdCategory));//set combobox values to enums
            addProductButton.Visibility = Visibility.Collapsed;//add invisible
            updateProductButton.Visibility = Visibility.Visible;//show update
            ID.IsReadOnly = true;//cant change id of a product to update
            tinstock.Text = prod.InStock.ToString();
        }

        private void tid_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text); //making sure it only only gets numbers for id
        }

        private void tinstock_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text); //making sure it only gets numbers for instock
        }

        private void tprice_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text); //making sure it only only gets numbers for price
        }
        private void tname_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^a-z]+[A-Z]+").IsMatch(e.Text); //making sure it only only get letters for name
        }

        private void uinstock_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text); //making sure it only only gets numbers for instock
        }

        private void uprice_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);//making sure it only only gets numbers for price
        }
        private void uname_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                e.Handled = new Regex("[^a-z]+[^A-Z]+").IsMatch(e.Text); //making sure it only only get letters for name 
            }
            catch (BO.InvalidInputException exc)
            {
                new ErrorWindow("Product List View Window\n", exc.Message).ShowDialog();
            }
        }

        //the following functions are handling text user inputted for new product fields and updating product fields
        private void tname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tname != null && tname.Text != "")
            {
                product.Name = tname.Text;
            }
        }

        private void tprice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tprice != null && tprice.Text != "")
            {
                if (int.TryParse(tprice.Text, out int val))
                {
                    product.Price = val;
                }
            }
        }

        private void tinstock_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tinstock != null && tinstock.Text != "")
            {
                if (int.TryParse(tinstock.Text, out int val))
                {
                    product.InStock = val;
                }
            }
        }

        private void uname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (uname != null && uname.Text != "")
            {
                product.Name = uname.Text;
            }
            else
            {
                product.Name = tname.Text;
            }
        }

        private void uprice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (uprice != null && uprice.Text != "")
            {
                if (int.TryParse(uprice.Text, out int val))
                {
                    product.Price = val;
                }
            } 
        }

        private void uinstock_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (uinstock != null && uinstock.Text != "")
            {
                if (int.TryParse(uinstock.Text, out int val))
                {
                    product.InStock = val;
                }
            }
            
        }

        private void tname_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            tname.Clear();
        }

        private void tprice_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            tprice.Clear();
        }

        private void tinstock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            tinstock.Clear();
        }

        private void SelectCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Enums.ProdCategory productCategory = (BO.Enums.ProdCategory)CategoryBox.SelectedItem; //saves the selected category
            product.Category = productCategory;
        }

        /// <summary>
        /// method handling the add product button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl!.products.AddProduct(product); //add BO product 
            }
            catch (BO.InvalidInputException ex) //InvalidInput error on the screen 
            {
                new ErrorWindow("Add Product Window\n", ex.Message).ShowDialog();
            }
            catch (BO.BOEntityAlreadyExistsException ex) //EntityAlreadyExistsException error on the screen 
            {
                new ErrorWindow("Add Product Window\n", ex.Message).ShowDialog();
            }
            //trigger of a pop up message
            ID.Text = "Enter ID";
            tname.Text = "Enter Name";
            tprice.Text = "Enter Price";
            tinstock.Text = "Enter Amount"; 
            Close(); //close this window
            //new ProductListWindow().Show(); //open productList back up to see newly added product
        }

        /// <summary>
        /// method handling the update product button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateProductButton_Click(object sender, RoutedEventArgs e)
        {
            try    
            {
                bl!.products.UpdateProduct(product); //update BO product
            }
            catch (BO.InvalidInputException ex) //InvalidInput error on the screen 
            {
                new ErrorWindow("Update Product Window\n", ex.Message).ShowDialog();
            }
            catch (BO.BOEntityDoesNotExistException ex) //EntityDoesNotExistException error on the screen 
            {
                new ErrorWindow("Update Product Window\n", ex.Message).ShowDialog();
            }
            ID.Text = "Enter ID";
            tname.Text = "Enter Name";
            tprice.Text = "Enter Price";
            tinstock.Text = "Enter Amount";
            Close(); //close this window
        }
    }
}


