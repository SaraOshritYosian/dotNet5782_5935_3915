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
    /// Interaction logic for ParcelWindow.xaml
    /// </summary>
    public partial class ParcelWindow : Window
    {
       
        private IBL accseccBL2;
        ParcelListWindow ParcelListWindow;
        BO.ParcelToLIst ParcelToLIst;
        public ParcelWindow(IBL accseccBL, BO.ParcelToLIst parcel ,ParcelListWindow parcelList)//update
        {
            InitializeComponent();
           
            accseccBL2 = accseccBL;
            ParcelListWindow = parcelList;
            ParcelToLIst = parcel;
            GridUpParcel.Visibility = Visibility.Visible;//עדכון מופעל
            GridAddParcel.Visibility = Visibility.Hidden;//הוספה מופעל
            GridUpParcel.DataContext = parcel;
            IdsLabel.Content = accseccBL.GetParcel(parcel.Id).CustomerInParcelSender;
            IdTLabel.Content= accseccBL.GetParcel(parcel.Id).CustomerInParcelTarget;
            if (accseccBL.GetParcel(parcel.Id).DroneInParcel != null &&  accseccBL.GetParcel(parcel.Id).Delivered==default)
            {
                DroneLabel.Content = accseccBL.GetDrone(accseccBL.GetParcel(parcel.Id).DroneInParcel.Id).Id ;
                lAbelDrone.Visibility = Visibility.Visible;
            }
            else
            {
                DroneLabel.Visibility = Visibility.Hidden;
                lAbelDrone.Visibility = Visibility.Hidden;
            }
               
            //DroneLabel.Content = accseccBL.GetParcel(parcel.Id).DroneInParcel;
            PichedUpT.Content = accseccBL.GetParcel(parcel.Id).PichedUp;
            ScheduledT.Content = accseccBL.GetParcel(parcel.Id).Scheduled;
            DeliveredT.Content = accseccBL.GetParcel(parcel.Id).Delivered;
            RequestedT.Content = accseccBL.GetParcel(parcel.Id).Requested;
            if (accseccBL2.GetParcel(ParcelToLIst.Id).Scheduled == default)//אם לא שוייך ניתן למחוק
                deletParcel.Visibility = Visibility.Visible;
            else
                deletParcel.Visibility = Visibility.Hidden;
        }
        public ParcelWindow(IBL accseccBL, BO.ParcelToLIst parcel)//ניפתח תצוגת חבילה מהחלון של הרחפן
        {
            InitializeComponent();

            accseccBL2 = accseccBL;
            ParcelToLIst = parcel;
            GridUpParcel.Visibility = Visibility.Visible;//עדכון מופעל
            GridAddParcel.Visibility = Visibility.Hidden;//הוספה מופעל
            DroneLabel.IsEnabled = false; ;
            IdsLabel.IsEnabled = false;
            IdTLabel.IsEnabled = false;
            GridUpParcel.DataContext = parcel;
            IdsLabel.Content = accseccBL.GetParcel(parcel.Id).CustomerInParcelSender;
            IdTLabel.Content = accseccBL.GetParcel(parcel.Id).CustomerInParcelTarget;
            if (accseccBL.GetParcel(parcel.Id).DroneInParcel != null && accseccBL.GetParcel(parcel.Id).Delivered == default)
            {
                DroneLabel.Content = accseccBL.GetDrone(accseccBL.GetParcel(parcel.Id).DroneInParcel.Id).Id;
                lAbelDrone.Visibility = Visibility.Visible;
            }
            else
            {
                DroneLabel.Visibility = Visibility.Hidden;
                lAbelDrone.Visibility = Visibility.Hidden;
            }

            //DroneLabel.Content = accseccBL.GetParcel(parcel.Id).DroneInParcel;
            PichedUpT.Content = accseccBL.GetParcel(parcel.Id).PichedUp;
            ScheduledT.Content = accseccBL.GetParcel(parcel.Id).Scheduled;
            DeliveredT.Content = accseccBL.GetParcel(parcel.Id).Delivered;
            RequestedT.Content = accseccBL.GetParcel(parcel.Id).Requested;
            deletParcel.Visibility = Visibility.Hidden;
        }
        public ParcelWindow(IBL accseccBL, ParcelListWindow parcelList)//add
        {
            InitializeComponent();
            Width = 600;
            accseccBL2 = accseccBL;
            ParcelListWindow = parcelList;
            GridUpParcel.Visibility = Visibility.Hidden;//עדכון מופעל
            GridAddParcel.Visibility = Visibility.Visible;//הוספה מופעל
            ComboBoxWeight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            ComboBoxproperty.ItemsSource = Enum.GetValues(typeof(BO.Priority));
            Label2.Visibility = Visibility.Hidden;
            Label3.Visibility = Visibility.Hidden;
            Label1.Visibility = Visibility.Hidden;
            Label4.Visibility = Visibility.Hidden;
            Label5.Visibility = Visibility.Hidden;
            Label6.Visibility = Visibility.Hidden;

        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)//כפתור הוספת חבילה
        {
            if (TexIdt.Text == "")
                Label3.Visibility = Visibility.Visible;
            if (TextIds.Text == "")
                Label4.Visibility = Visibility.Visible;
            if (ComboBoxWeight.SelectedItem == null)
                Label2.Visibility = Visibility.Visible;
            if (ComboBoxproperty.SelectedItem == null)
                Label1.Visibility = Visibility.Visible;

            if (TexIdt.Text != "")
                Label3.Visibility = Visibility.Hidden;
            if (ComboBoxWeight.SelectedItem != null)
                Label2.Visibility = Visibility.Hidden;
            if (ComboBoxproperty.SelectedItem != null)
                Label1.Visibility = Visibility.Hidden;
            if (TextIds.Text != "")
            {

                Label4.Visibility = Visibility.Hidden;
                if (accseccBL2.GetCustomers().Any(p => p.Id == Convert.ToInt32(TextIds.Text))==false)//לא קיים
                {
                    Label5.Visibility = Visibility.Visible;
                }
                else
                    Label5.Visibility = Visibility.Hidden;

            }
            if (TexIdt.Text != "")//לא ריק לבדור אם ת"ז קיים
            {


                Label3.Visibility = Visibility.Hidden;
                if (accseccBL2.GetCustomers().Any(p => p.Id == Convert.ToInt32(TexIdt.Text))==false)//לא קיים
                {
                    Label6.Visibility = Visibility.Visible;
                }
                else
                    Label6.Visibility = Visibility.Hidden;

            }

            if (TextIds.Text != "")
            {
                if ((TexIdt.Text != "") & (ComboBoxWeight.SelectedItem != null) & (ComboBoxproperty.SelectedItem != null) & (accseccBL2.GetCustomers().Any(p => p.Id == Convert.ToInt32(TextIds.Text)) == true) & (accseccBL2.GetCustomers().Any(p => p.Id == Convert.ToInt32(TexIdt.Text)) == true))
                {
                    BO.Parcel parcel1;
                    parcel1 = new() {  CustomerInParcelSender=new BO.CustomerInParcel { Id = Convert.ToInt32(TextIds.Text), Name = accseccBL2.GetCustomer(Convert.ToInt32(TextIds.Text)).Name }
                    ,CustomerInParcelTarget= new BO.CustomerInParcel { Id = Convert.ToInt32(TexIdt.Text), Name = accseccBL2.GetCustomer(Convert.ToInt32(TexIdt.Text)).Name },
                        Weight = (BO.WeightCategories)ComboBoxWeight.SelectedItem,Priority= (BO.Priority)ComboBoxproperty.SelectedItem
                    };
                    try
                    {
                       int i= accseccBL2.AddParcel(parcel1);//parcel add

                        MessageBox.Show("Adding a Parcel number: " + i + " was successful");

                        ParcelListWindow.parcelToLIstDataGrid.DataContext = accseccBL2.GetParcels();
                        Close();
                    }
                   
                      catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);

                    }

                }
            }
        
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            var a = MessageBox.Show("You're sure you want to close", "closr", MessageBoxButton.YesNo);

            if (a == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var a = MessageBox.Show("You're sure you want to close", "closr", MessageBoxButton.YesNo);

            if (a == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void DroneLabel_Click(object sender, RoutedEventArgs e)
        {
            DronWindow dronWindow = new DronWindow(accseccBL2, accseccBL2.DroneToLisToPrint(accseccBL2.GetParcel(ParcelToLIst.Id).DroneInParcel.Id));
            dronWindow.Show();
        }

        private void IdsLabel_Click(object sender, RoutedEventArgs e)//השולח
        {
            BO.CustomerToList customerToList = accseccBL2.CostumerToListToPrint(accseccBL2.GetParcel(ParcelToLIst.Id).CustomerInParcelSender.Id);
            CustomerWindow customerWindow = new(accseccBL2, customerToList);
            customerWindow.Show();
        }

        private void IdTLabel_Click(object sender, RoutedEventArgs e)//המקבל
        {
            BO.CustomerToList customerToList = accseccBL2.CostumerToListToPrint(accseccBL2.GetParcel(ParcelToLIst.Id).CustomerInParcelTarget.Id);
            CustomerWindow customerWindow = new(accseccBL2, customerToList);
            customerWindow.Show();
        }

        private void deletParcel_Click(object sender, RoutedEventArgs e)
        {
            if (accseccBL2.GetParcel(ParcelToLIst.Id).Scheduled == default)//אם לא שוייך ניתן למחוק
            {
                var a = MessageBox.Show("You're sure you want to delete", "delete", MessageBoxButton.YesNo);

                if (a == MessageBoxResult.Yes)
                {
                    accseccBL2.DeleteParcel(ParcelToLIst.Id);
                    ParcelListWindow.parcelToLIstDataGrid.DataContext = accseccBL2.GetParcels();
                    MessageBox.Show("The order was successfully deleted");
                    this.Close();
                }


            }
            else
                MessageBox.Show("This invitation could not be deleted because it was associated with Drone");

        }
    }
}
