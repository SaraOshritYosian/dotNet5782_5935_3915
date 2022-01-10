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
    /// Interaction logic for StationListWindow.xaml
    /// </summary>
    /// 
    public partial class StationListWindow : Window
    {
        private IBL accseccBL1;
        public StationListWindow(IBL accseccBL)
        {
            
            InitializeComponent();
           
            accseccBL1 = accseccBL;
            var stationList = accseccBL1.GetStations();
            DataGrideStation.ItemsSource = stationList;
        }

       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ListsWindow we = new ListsWindow(accseccBL1);
            we.Show();
            Close();
        }

        private void AddS_Click(object sender, RoutedEventArgs e)
        {
            StationWindow we = new StationWindow(accseccBL1,this);
            we.Show();
        }

        private void DataGrideStation_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            BO.StationToList st = DataGrideStation.SelectedItem as BO.StationToList;
            StationWindow we = new StationWindow(accseccBL1, this, st);//פעולה בונה של הוספה
            we.Show();
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

        private void DataGrideStation_MouseEnter(object sender, MouseEventArgs e)
        {
            BO.StationToList st = DataGrideStation.SelectedItem as BO.StationToList;
           
           
        }

        private void DataGrideStation_MouseLeave(object sender, MouseEventArgs e)
        {

        }   }
}
