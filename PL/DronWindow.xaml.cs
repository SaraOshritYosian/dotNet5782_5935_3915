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
using IBL.BO;
namespace PL
{
    /// <summary>
    /// Interaction logic for DronWindow.xaml
    /// </summary>
   
    public partial class DronWindow : Window
    {
        DroneListWindow droneListWindow11;
        static int idDrone = 11;
        IBL.BO.DroneToList droneTo;
        IBL.BL accseccBL2;
        TimeSpan timee;
        #region Add
        public DronWindow(IBL.BL accseccBL1,DroneListWindow dd)//add
        {
           
            InitializeComponent();
            GridUpDrone.Visibility = Visibility.Hidden;//עדכון מופעל
            droneListWindow11 = dd;//מקבל חלון של דרון ליסט
           accseccBL2 = accseccBL1;
            ComboBoxWeight.ItemsSource = Enum.GetValues(typeof(Enums.WeightCategories));
            Label2.Visibility = Visibility.Hidden;
            Label3.Visibility = Visibility.Hidden;
            Label1.Visibility = Visibility.Hidden;

            List<int> aa = new List<int>();
            for (int i = 0; i < accseccBL2.AvailableStationToChargeList().Count(); i++)
            {
                ComboBoxStation.Items.Add(accseccBL2.AvailableStationToChargeList().ElementAt(i).Id);
            }
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {

            if (TexModel.Text == "")
                Label3.Visibility = Visibility.Visible;
            if (ComboBoxWeight.SelectedItem == null)
                Label2.Visibility = Visibility.Visible;
            if (ComboBoxStation.SelectedItem == null)
                Label1.Visibility = Visibility.Visible;
            if (TexModel.Text != "")
                Label3.Visibility = Visibility.Hidden;
            if (ComboBoxWeight.SelectedItem != null)
                Label2.Visibility = Visibility.Hidden;
            if (ComboBoxStation.SelectedItem != null)
                Label1.Visibility = Visibility.Hidden;
            if ((ComboBoxWeight.SelectedItem != null) & (ComboBoxStation.SelectedItem != null) & TexModel.Text != "")
            {
                Drone drone1 = new Drone();
                drone1.Id = idDrone;//id
                idDrone++;
                drone1.Model = TexModel.Text;//model
                drone1.Weight = (Enums.WeightCategories)ComboBoxWeight.SelectedItem;//weight
                int station = Convert.ToInt32(ComboBoxStation.SelectedItem);
                try
                {
                    accseccBL2.AddDrone(drone1, (int)ComboBoxStation.SelectedItem);//station
                    MessageBox.Show("add drone OK");
                    DroneListWindow win = new DroneListWindow(accseccBL2);
                    win.Refresh(accseccBL2);
                    droneListWindow11.Close();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                    
                }
               
               
              
            }
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }
        #endregion
        public void Refresh(IBL.BL accseccBL1, IBL.BO.DroneToList drone)//מרענן את הדף
        {
            LabelErrorTime.Visibility = Visibility.Hidden;
            ButtonUpdate.Visibility = Visibility.Hidden;
            GridAddDrone.IsEnabled = true;
            GridAddDrone.Visibility = Visibility.Hidden;//עדכון מופעל
            comoboxTime.Visibility = Visibility.Hidden;
            LabelTime.Visibility = Visibility.Hidden;
            accseccBL2 = accseccBL1;
            droneTo = drone;
            LabelId2.Content = Convert.ToString(droneTo.Id);
            LabelWeight2.Content = droneTo.Weight;//משקל
            LabeLocation2.Content = droneTo.LocationDrone;//מיקופ
            
            TexBoxModel.Text = droneTo.Model;//מודל
                                             //אם יש שינוי

            if (droneTo.StatusDrone == Enums.StatusDrone.InMaintenance)
                statuse.Content = " הרחפן בטעינה";
            if (droneTo.StatusDrone == Enums.StatusDrone.available)
                statuse.Content = " הרחפן  זמין";
            if (droneTo.StatusDrone == Enums.StatusDrone.delivered)
            {
                int a = droneTo.IdParcel;
                if (a != 0)
                {
                    Enums.StatusParcel pp = accseccBL2.StatuseParcelKnow(a);

                    if (pp == Enums.StatusParcel.collected)//סופק
                        statuse.Content = "הרחפן בדרך לספק את המשלוח ללקוח";
                    else
                        statuse.Content = " הרחפן  בדרך לאסוף את המשלוח מהלקוח";
                }

            }
            TexBattery.Text = droneTo.StatusBatter.ToString() + "%";//בטריה
                                                                    // TexBoxModel.IsReadOnly = true;//טקסט מודל רק לקריאה
            LinearGradientBrush myBrush = new LinearGradientBrush();//צבע בטריה
            if (droneTo.StatusDrone == Enums.StatusDrone.InMaintenance)//אם בתחזוקה אז יש אפשרות לשחחרר רחםן בטעינה
            {
                BottonToFun.Content = "Release from charging";
                comoboxTime.Visibility = Visibility.Visible;
                LabelTime.Visibility = Visibility.Visible;
                int[] a = { 1, 2, 5, 7, 10 };
                comoboxTime.ItemsSource = a;
                BottonToFun2.Visibility = Visibility.Hidden;
            }
            if (droneTo.StatusDrone == Enums.StatusDrone.available)//אם זמין אז יש אפשרות או לשייך חבילה או לשלוח לטעינה
            {
                BottonToFun.Content = "Send for loading";
                BottonToFun2.Content = "Assignment to the package";
            }
            if (droneTo.StatusDrone == Enums.StatusDrone.delivered)//אם במצב משלוח אז יש אפשרות או לאסוף חבילה או לספק
            {
                BottonToFun.Content = "Collect a package";
                BottonToFun2.Content = "Provide package";
            }
            TexBattery.Background = myBrush;
            if (droneTo.StatusBatter < 21)
            {
                myBrush.GradientStops.Add(new GradientStop(Colors.Red, 1.0));
                TexBattery.Background = myBrush;
                //TexBattery
            }

            if ((droneTo.StatusBatter > 21) & (droneTo.StatusBatter < 80))
            {
                myBrush.GradientStops.Add(new GradientStop(Colors.Orange, 0.5));
                TexBattery.Background = myBrush;
            }

            if (droneTo.StatusBatter < 100 & droneTo.StatusBatter > 80)
            {
                myBrush.GradientStops.Add(new GradientStop(Colors.Green, 0.0));
                TexBattery.Background = myBrush;
            }


        }

        public DronWindow(IBL.BL accseccBL1, IBL.BO.DroneToList drone,DroneListWindow droneList)//update
        {


            InitializeComponent();
            droneListWindow11 = droneList;//מקבל את החלון רשימת רחפן כשי שיוכל לסגור ברענון
            LabelErrorTime.Visibility = Visibility.Hidden;
            ButtonUpdate.Visibility = Visibility.Hidden;
            GridAddDrone.IsEnabled = true;
            GridAddDrone.Visibility = Visibility.Hidden;//עדכון מופעל
            comoboxTime.Visibility = Visibility.Hidden;
            LabelTime.Visibility = Visibility.Hidden;
            LabelErrorMode.Visibility = Visibility.Hidden;
            accseccBL2 = accseccBL1;
            droneTo = drone;
            LabelId2.Content = Convert.ToString(droneTo.Id);
            LabelWeight2.Content = droneTo.Weight;//משקל
            LabeLocation2.Content = droneTo.LocationDrone;//מיקופ
            //TexBoxModel.IsVisibleChanged +=true;
            TexBoxModel.Text = droneTo.Model;//מודל
                                             //אם יש שינוי

            if (droneTo.StatusDrone == Enums.StatusDrone.InMaintenance)
                statuse.Content = " הרחפן בטעינה";
            if (droneTo.StatusDrone == Enums.StatusDrone.available)
                statuse.Content = " הרחפן  זמין";
            if (droneTo.StatusDrone == Enums.StatusDrone.delivered)
            {
                int a = droneTo.IdParcel;
                if (a != 0)
                {
                    Enums.StatusParcel pp = accseccBL2.StatuseParcelKnow(a);

                    if (pp == Enums.StatusParcel.collected)//סופק
                        statuse.Content = "הרחפן בדרך לספק את המשלוח ללקוח";
                    else
                        statuse.Content = " הרחפן  בדרך לאסוף את המשלוח מהלקוח";
                }

            }
            TexBattery.Text = droneTo.StatusBatter.ToString() + "%";//בטריה
                                                                    // TexBoxModel.IsReadOnly = true;//טקסט מודל רק לקריאה
            LinearGradientBrush myBrush = new LinearGradientBrush();//צבע בטריה
            if (droneTo.StatusDrone == Enums.StatusDrone.InMaintenance)//אם בתחזוקה אז יש אפשרות לשחחרר רחםן בטעינה
            {
                BottonToFun.Content = "Release from charging";
                comoboxTime.Visibility = Visibility.Visible;
                LabelTime.Visibility = Visibility.Visible;
                int[] a = { 1, 2, 5, 7, 10 };
                comoboxTime.ItemsSource = a;
                BottonToFun2.Visibility = Visibility.Hidden;
            }
            if (droneTo.StatusDrone == Enums.StatusDrone.available)//אם זמין אז יש אפשרות או לשייך חבילה או לשלוח לטעינה
            {
                BottonToFun.Content = "Send for loading";
                BottonToFun2.Content = "Assignment to the package";
            }
            if (droneTo.StatusDrone == Enums.StatusDrone.delivered)//אם במצב משלוח אז יש אפשרות או לאסוף חבילה או לספק
            {
                BottonToFun.Content = "Collect a package";
                BottonToFun2.Content = "Provide package";
            }
            TexBattery.Background = myBrush;
            if (droneTo.StatusBatter < 21)
            {
                myBrush.GradientStops.Add(new GradientStop(Colors.Red, 1.0));
                TexBattery.Background = myBrush;
                //TexBattery
            }

            if ((droneTo.StatusBatter > 21) & (droneTo.StatusBatter < 80))
            {
                myBrush.GradientStops.Add(new GradientStop(Colors.Orange, 0.5));
                TexBattery.Background = myBrush;
            }

            if (droneTo.StatusBatter < 100 & droneTo.StatusBatter > 80)
            {
                myBrush.GradientStops.Add(new GradientStop(Colors.Green, 0.0));
                TexBattery.Background = myBrush;
            }

        }
        private void ComboBoxWeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //שומר את הבחירה
        }

        private void BottonToFun_Click(object sender, RoutedEventArgs e)
        {

            if (BottonToFun.Content == "Release from charging")//שחרור
            {


                if (comoboxTime.SelectedItem != null)
                {
                     timee = (TimeSpan)comoboxTime.SelectedItem;
                     
                    try
                    {
                        accseccBL2.ReleaseDrone(droneTo.Id, timee);
                        MessageBox.Show("fun Succeeded");
                        DroneListWindow win = new DroneListWindow(accseccBL2);
                        win.Refresh(accseccBL2);
                        this.Refresh(accseccBL2, accseccBL2.DroneToLisToPrint(droneTo.Id));
                        droneListWindow11.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    LabelErrorTime.Visibility = Visibility.Visible;
                }


            }
            if (BottonToFun.Content == ("Send for loading"))//שליחה
            {
                try
                {
                    accseccBL2.SendingDroneToCharging(droneTo.Id);
                }
                 catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                MessageBox.Show("fun Succeeded");
                DroneListWindow win = new DroneListWindow(accseccBL2);
                win.Refresh(accseccBL2);
                droneListWindow11.Close();
                this.Close();

            }
            if (BottonToFun.Content == ("Collect a package"))//לאסוף
            {
                try
                {
                    accseccBL2.PickUpPackage(droneTo.Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                MessageBox.Show("fun Succeeded");
                DroneListWindow win = new DroneListWindow(accseccBL2);
                win.Refresh(accseccBL2);
                droneListWindow11.Close();
                this.Close();

            }
        }

  


        private void BottonToFun2_Click(object sender, RoutedEventArgs e)
        {

            if (BottonToFun2.Content == ("Assignment to the package"))//שיוך
            {
                try
                {
                    accseccBL2.AssignPackageToDrone(droneTo.Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                MessageBox.Show("fun Succeeded");
                DroneListWindow win = new DroneListWindow(accseccBL2);
                win.Refresh(accseccBL2);
                droneListWindow11.Close();
                this.Close();
            }

            if (BottonToFun2.Content == ("Provide package"))
            {
                try
                {
                    accseccBL2.PackageDeliveryByDrone(droneTo.Id);//לספק
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                MessageBox.Show("fun Succeeded");
                DroneListWindow win = new DroneListWindow(accseccBL2);
                win.Refresh(accseccBL2);
                droneListWindow11.Close();
                this.Close();
            }
          
        }

        private void ButtoneMod_Click(object sender, RoutedEventArgs e)
        {
            ButtonUpdate.Visibility = Visibility.Visible;
           string model = TexBoxModel.Text;
            //TexBoxModel.Visibility = Visibility.Hidden;
            TexBoxModel.Clear();
            ButtoneMod.Visibility = Visibility.Hidden;
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
           
            if (TexBoxModel.Text != null)
            {
                string modelnew = TexBoxModel.Text;
                try
                {
                    accseccBL2.UpdateDrone(droneTo.Id, modelnew);
                    MessageBox.Show("update drone OK");
                    //ButtoneMod.Visibility = Visibility.Visible;
                    //ButtonUpdate.Visibility = Visibility.Hidden;
                    DroneListWindow win = new DroneListWindow(accseccBL2);
                    win.Refresh(accseccBL2);
                    this.Refresh(accseccBL2, accseccBL2.DroneToLisToPrint(droneTo.Id));
                    droneListWindow11.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            else
                LabelErrorMode.Visibility = Visibility.Visible;


        }

        private void ComboBoxWeight_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Cancell_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
