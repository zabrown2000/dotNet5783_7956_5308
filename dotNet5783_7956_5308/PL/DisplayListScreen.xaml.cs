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
                ItemsListView.ItemsSource = bltemp.products.ReadProductsForList(); //get products for list from BO
            }
            catch (BO.Exceptions ex)//id is null error on screen
            {
                //new ErrorWindow("List View Window\n", ex.Message).ShowDialog();
                //Console.WriteLine("List View Window\n");
                //Console.WriteLine(ex.Message);
                //Console.WriteLine("getting products failed-id is null\n");
                //Console.WriteLine(ex.InnerException?.ToString());
            }
            //AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        }

    }

    private void ItemsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

