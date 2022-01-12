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
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow : Window
    {
        private IBL accseccBL1;
        private  StationListWindow statwin;
        BO.StationToList toList;
        public StationWindow(IBL accseccBL, StationListWindow wen)//add
        {
            InitializeComponent();
            accseccBL1 = accseccBL;
            GridUpStation.Visibility = Visibility.Hidden;
            GridAddSattion.Visibility = Visibility.Visible;
            statwin = wen;
            Label2.Visibility = Visibility.Hidden;
            Label3.Visibility = Visibility.Hidden;
            Label1.Visibility = Visibility.Hidden;
            Label4.Visibility = Visibility.Hidden;
            Label5.Visibility = Visibility.Hidden;

            Label6.Visibility = Visibility.Hidden;
           
        }

        public StationWindow(IBL accseccBL, StationListWindow wen, BO.StationToList to)//update
        {

            InitializeComponent();
            lbb.Content= "Number of\ncharging stations:";
            this.Width = 800;
            Height = 450;
            GridUpStation.Visibility = Visibility.Visible;
            GridAddSattion.Visibility = Visibility.Hidden;
            accseccBL1 = accseccBL;
            statwin = wen;
            toList = to;
            GridUpStation.DataContext = to;//id,name,
            ListViewDroneInCharge.ItemsSource = accseccBL1.ListDroneInStation(toList.Id);
            IdText.IsReadOnly = true;
            LocationText.Content = accseccBL.GetStation(to.Id).LocationStation;
            ChargeSlote.Text = (to.ChargeSlotsFree + to.ChargeSlotsNotFree).ToString();
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            GridAddSattion.Visibility = Visibility.Visible;
            GridUpStation.Visibility = Visibility.Hidden;

            if (TexName.Text == "")
                Label3.Visibility = Visibility.Visible;
            if (TextId.Text == "")
                Label4.Visibility = Visibility.Visible;
            if (TextLocation1.Text == "")
                Label2.Visibility = Visibility.Visible;
            if (TextLocation2.Text == "")
                Label6.Visibility = Visibility.Visible;
            if (textSlots.Text == "")
                Label1.Visibility = Visibility.Visible;

            if (TexName.Text != "")
                Label3.Visibility = Visibility.Hidden;
            if (TextLocation1.Text != "")
                Label2.Visibility = Visibility.Hidden;
            if (TextLocation2.Text != "")
                Label6.Visibility = Visibility.Hidden;
            if (textSlots.Text != "")
                Label1.Visibility = Visibility.Hidden;
            if (TextId.Text != "")
            {


                Label4.Visibility = Visibility.Hidden;
                if (accseccBL1.GetStations().Any(p => p.Id == Convert.ToInt32(TextId.Text)))//קיים
                {
                    Label5.Visibility = Visibility.Visible;
                }
                else
                    Label5.Visibility = Visibility.Hidden;
            }

            if (TextId.Text != "")
            {
                if ((TextId.Text != "") & (TextLocation1.Text != "") & (TextLocation1.Text != "")& (TextLocation1.Text != "") & (textSlots.Text != "") & (TexName.Text != "") & (accseccBL1.GetStations().Any(p => p.Id == Convert.ToInt32(TextId.Text)) == false))
                {
                    BO.Station station;
                    station = new() { Id = Convert.ToInt32(TextId.Text), Name =Convert.ToInt32( TexName.Text), LocationStation =new BO.Location {Latitude=Convert.ToDouble( TextLocation1.Text),Longitude=Convert.ToDouble( TextLocation2.Text) }, ChargeSlotsFree =Convert.ToInt32( textSlots.Text) };
                    accseccBL1.AddStation(station);//station
                    MessageBox.Show("Adding a station number: " + station.Id + " was successful");
                    statwin.stationToListDataGrid.ItemsSource = accseccBL1.GetStations();
                    Close();

                }
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            accseccBL1.UpdateStation(toList.Id, Convert.ToInt32(TexName1.Text), Convert.ToInt32(ChargeSlote.Text));
            statwin.stationToListDataGrid.DataContext=accseccBL1.GetStations();
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)//לשלוח לחלון של רחפן את הרחפן הנוכלי
        {

            BO.DroneToList drne = ListViewDroneInCharge.SelectedItem as BO.DroneToList;
            if (drne != null)
            {
                DronWindow dr = new DronWindow(accseccBL1, drne);
                dr.Show();
            }
           
        }
    }
    
}
