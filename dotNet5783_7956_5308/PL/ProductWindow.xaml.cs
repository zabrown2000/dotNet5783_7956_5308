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
        private IBl bl = new Bl();
        private BO.Products product = new BO.Products();
        public ProductWindow() // constructor
        {
            InitializeComponent();
            CategoryBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProdCategory));
            updateProductButton.Visibility = Visibility.Collapsed;//update invisible 
            uinstock.Visibility = Visibility.Collapsed;
            uprice.Visibility = Visibility.Collapsed;
            uname.Visibility = Visibility.Collapsed;
            ID.Text = bl.products.GetNextID().ToString();
        }

        public ProductWindow(Products prod)
        {
            InitializeComponent();
            CategoryBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProdCategory));
            addProductButton.Visibility = Visibility.Collapsed;//add invisible
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
        private void tid_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);//only gets numbers for id
        }

        private void tinstock_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);//only gets numbers for instock
        }

        private void tprice_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);//only gets numbers for price
        }
        private void tname_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^a-z]+[A-Z]+").IsMatch(e.Text);//only get letters 
        }

        private void uinstock_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);//only gets numbers for instock
        }

        private void uprice_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);//only gets numbers for price
        }
        private void uname_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            //e.Handled = new Regex("[^0-9.-]+").IsMatch(e.Text);
            try
            {
                e.Handled = new Regex("[^a-z]+[^A-Z]+").IsMatch(e.Text);//only get letters  // HAD A PLUS AFTER THE []
            }
            catch (BO.InvalidInputException exc)
            {
                new ErrorWindow("Product List View Window\n", exc.Message).ShowDialog();
            }
        }


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
            } else
            {
                product.Price = int.Parse(tprice.Text);
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
            } else
            {
                product.InStock = int.Parse(tinstock.Text);
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
            BO.Enums.ProdCategory productCategory = (BO.Enums.ProdCategory)CategoryBox.SelectedItem; // saves the selected category
            product.Category = productCategory;
        }


        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl!.products.AddProduct(product);//add product to BO
            }
            catch (BO.InvalidInputException ex)//IncorrectInput error on the screen 
            {
                new ErrorWindow("Add Product Window\n", ex.Message).ShowDialog();
                //Console.WriteLine("Add Product Window\n");
                //Console.WriteLine(ex.Message);
                //Console.WriteLine("your input is incorrect\n");
                //Console.WriteLine(ex.InnerException?.ToString());
            }
            catch (BO.BOEntityAlreadyExistsException ex)//IdExistException error on the screen 
            {
                new ErrorWindow("Add Product Window\n", ex.Message).ShowDialog();
                //Console.WriteLine("Add Product Window\n");
                //Console.WriteLine(ex.Message);
                //Console.WriteLine("the ID you requested to add already exists\n");
                //Console.WriteLine(ex.InnerException?.ToString());
            }
            //trigger of a pup op message
            ID.Text = "Enter ID";
            tname.Text = "Enter Name";
            tprice.Text = "Enter Price";
            tinstock.Text = "Enter Amount";//returned previous text
            Close();//close this window

            /*try
            {
                bl.products.AddProduct(product);
            }
            catch (BO.InvalidInputException exc)
            {
                new ErrorWindow("Add Product Window\n", exc.Message).ShowDialog();
            }
            Close();*/
        }

        private void UpdateProductButton_Click(object sender, RoutedEventArgs e)
        {
            try    
            {
                bl!.products.UpdateProduct(product);//add product to BO
            }
            catch (BO.InvalidInputException ex)//IncorrectInput error on the screen 
            {
                new ErrorWindow("Add Product Window\n", ex.Message).ShowDialog();
                //Console.WriteLine("Add Product Window\n");
                //Console.WriteLine(ex.Message);
                //Console.WriteLine("your input is incorrect\n");
                //Console.WriteLine(ex.InnerException?.ToString());
            }
            catch (BO.BOEntityDoesNotExistException ex)//IdExistException error on the screen 
            {
                new ErrorWindow("Add Product Window\n", ex.Message).ShowDialog();
                //Console.WriteLine("Add Product Window\n");
                //Console.WriteLine(ex.Message);
                //Console.WriteLine("the ID you requested to update does not exists\n");
                //Console.WriteLine(ex.InnerException?.ToString());
            }
            ID.Text = "Enter ID";
            tname.Text = "Enter Name";
            tprice.Text = "Enter Price";
            tinstock.Text = "Enter Amount";//returned previous text
            Close();//close this window

            //bl.products.UpdateProduct(product); //problem: the fields are empty and throwing exception
            //Close();
        }
    }
}


