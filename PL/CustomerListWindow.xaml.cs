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
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerListWindow.xaml
    /// </summary>
    public partial class CustomerListWindow : Window
    {
        private IBL accseccBL1;
       
        public CustomerListWindow(IBL accseccBL)
        {

            InitializeComponent();
            accseccBL1 = accseccBL;
            var list= accseccBL1.GetCustomers();
            CustomerListView.DataContext = list;
            CustomerListView.IsReadOnly = true;


        }

        private void Cancell_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
        private void AddButton_Click(object sender, RoutedEventArgs e)//הוספת לקוח בלחיצה אחת
        {
            CustomerWindow cw = new CustomerWindow(accseccBL1, this);//לעשות את הפעולה 
            cw.Show();

        }

       

        private void CustomerListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

       

        private void CustomerListView_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            BO.CustomerToList cl = CustomerListView.SelectedItem as BO.CustomerToList;
            if (cl != null)
            {
                CustomerWindow cw = new CustomerWindow(accseccBL1, cl, this);//לעשות את הפעולה 
                cw.Show();
            }

        }
    }
}
