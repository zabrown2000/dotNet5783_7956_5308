using BlApi;
using BlImplementation;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for DisplayListScreen.xaml
    /// </summary>
    public partial class DisplayListScreen : Window
    {
        private IBl bltemp;

        public DisplayListScreen(IBl bl)
        {
            bltemp = bl;

            InitializeComponent();
            try
            {
                ProdListView.ItemsSource = bltemp.products.ReadProductsForList(); //get products for list from BO
            }
            catch (BO.Exceptions ex)//id is null error on screen
            {
                //new ErrorWindow("List View Window\n", ex.Message).ShowDialog();
                //Console.WriteLine("List View Window\n");
                //Console.WriteLine(ex.Message);
                //Console.WriteLine("getting products failed-id is null\n");
                //Console.WriteLine(ex.InnerException?.ToString());
            }
            Category.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProdCategory));
        }

        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Enums.ProdCategory c = (BO.Enums.ProdCategory)Category.SelectedItem;//save the category picked

            if (c == BO.Enums.ProdCategory.None)//if selected to view all products 
            {
                try
                {
                    ProdListView.ItemsSource = bltemp.products.ReadProductsForList(); //get products for list from BO
                }
                catch (BO.Exceptions ex)
                {
                    //new ErrorWindow("List View Window\n", ex.Message).ShowDialog();
                    //Console.WriteLine("List View Window\n");
                    //Console.WriteLine(ex.Message);
                    //Console.WriteLine("getting products failed-id is null\n");
                    //Console.WriteLine(ex.InnerException?.ToString());
                    //id is null error on screen
                }
                Category.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProdCategory));//show all of combobox options
                return;
            }
            if (c is BO.Enums.ProdCategory ca)
            {
                try
                {
                    ProdListView.ItemsSource = bltemp.products.ReadProductsForList().Select(x => x!.Category == ca);//show filtered list
                }
                catch (BO.Exceptions ex)
                {
                    //new ErrorWindow("List View Window\n", ex.Message).ShowDialog();
                    //Console.WriteLine("List View Window\n");
                    //Console.WriteLine(ex.Message);
                    //Console.WriteLine("getting products failed-id is null\n");
                    //Console.WriteLine(ex.InnerException?.ToString());
                    //id is null error on screen
                }
            }

            try
            {
                ProdListView.ItemsSource = from p in bltemp.products.ReadProductsForList()//get all products
                                           where p.Category == c
                                           select p;//selected all products of selected category
            }
            catch (BO.Exceptions ex)
            {
                //new ErrorWindow("List View Window\n", ex.Message).ShowDialog();
                //Console.WriteLine("List View Window\n");
                //Console.WriteLine(ex.Message);
                //Console.WriteLine("getting products failed-id is null\n");
                //Console.WriteLine(ex.InnerException?.ToString());
                ////id is null error on screen
            }
        }
    }

    }

