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
    /// Interaction logic for ParcelListWindow.xaml
    /// </summary>
    public partial class ParcelListWindow : Window
    {
        private IBL accseccBL1;
        public ParcelListWindow(IBL accseccBL)
        {
            InitializeComponent();
            accseccBL1 = accseccBL;
            parcelToLIstDataGrid.DataContext= accseccBL1.GetParcels();
            parcelToLIstDataGrid.IsReadOnly = true;
        }

       
        private void AddParcel_Click(object sender, RoutedEventArgs e)
        {
            ParcelWindow dr = new ParcelWindow(accseccBL1, this);//מקבל גם גישה וגם את החלון כדי שיוכל לסגור אותו
            dr.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ListsWindow we = new ListsWindow(accseccBL1);
            we.Show();
            Close();
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        private void ParcelListData_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            BO.ParcelToLIst parcel = parcelToLIstDataGrid.SelectedItem as BO.ParcelToLIst;
            ParcelWindow dr = new ParcelWindow(accseccBL1, parcel, this);
            dr.Show();
        }
    }
}
