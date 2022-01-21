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
            stationToListDataGrid.DataContext = stationList;
            stationToListDataGrid.IsReadOnly = true;
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


        private void Button_Click(object sender, RoutedEventArgs e)//סגירת חלון
        {
            var a = MessageBox.Show("You're sure you want to close", "closr", MessageBoxButton.YesNo);

            if (a == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//לחלון ראשי
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        
        private void Avisible_Checked(object sender, RoutedEventArgs e)//סינון
        {
            if (Avisible.IsChecked == true)
                stationToListDataGrid.DataContext = accseccBL1.AvailableStationToChargeListStationToList();
            else
                stationToListDataGrid.DataContext = accseccBL1.GetStations();
        }

       
        private void Avisible_Click(object sender, RoutedEventArgs e)
        {
            if (Avisible.IsChecked == true)
                stationToListDataGrid.DataContext = accseccBL1.AvailableStationToChargeListStationToList();
            else
                stationToListDataGrid.DataContext = accseccBL1.GetStations();
        }

        private void stationToListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)//לחיצה כפולה
        {
            BO.StationToList st = stationToListDataGrid.SelectedItem as BO.StationToList;
            if (st != null)
            {
                StationWindow we = new StationWindow(accseccBL1, this, st);//פעולה בונה של הוספה
                we.Show();
            }
            
        }
    }
}
