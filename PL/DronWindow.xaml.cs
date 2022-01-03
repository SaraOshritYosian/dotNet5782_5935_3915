﻿using System;
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
        private readonly DroneListWindow droneListWindow11;
       // static int idDrone = 11;
        DroneToList droneTo;
        IBL.BL accseccBL2;
        
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
            Label4.Visibility = Visibility.Hidden;
            Label5.Visibility = Visibility.Hidden;
            //List<int> aa = new List<int>();
            for (int i = 0; i < accseccBL2.AvailableStationToChargeList().Count(); i++)
            {
                ComboBoxStation.Items.Add(accseccBL2.AvailableStationToChargeList().ElementAt(i).Id);
            }
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
           // bool de = true;
            GridAddDrone.Visibility = Visibility.Visible;
            GridUpDrone.Visibility = Visibility.Hidden;
            
            if (TexModel.Text == "")
                Label3.Visibility = Visibility.Visible;
            if (TextId.Text == "")
                Label4.Visibility = Visibility.Visible;
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
            if (TextId.Text != "")
            {
                Label4.Visibility = Visibility.Hidden;
                if (accseccBL2.BlDrone.Any(p => p.Id == Convert.ToInt32(TextId.Text)))//קיים
                {
                    Label5.Visibility = Visibility.Visible;
                }
                else
                    Label5.Visibility = Visibility.Hidden;
            }

            if (TextId.Text != "")
            {
                if ((TextId.Text != "") & (ComboBoxWeight.SelectedItem != null) & (ComboBoxStation.SelectedItem != null) & (TexModel.Text != "") & (accseccBL2.BlDrone.Any(p => p.Id == Convert.ToInt32(TextId.Text)) == false))
                {
                    Drone drone1;
                    //bool dds = accseccBL2.BlDrone.Any(p => p.Id == Convert.ToInt32(TextId.Text));
                    //try
                    //{ 
                    drone1 = new() { Id = Convert.ToInt32(TextId.Text), Model = TexModel.Text, Weight = (Enums.WeightCategories)ComboBoxWeight.SelectedItem };
                    accseccBL2.AddDrone(drone1, (int)ComboBoxStation.SelectedItem);//station
                    MessageBox.Show("Adding a Drone number: " + drone1.Id + " was successful");
                    droneListWindow11.DronesListView.ItemsSource = accseccBL2.GetDrons();
                    Close();

                }
            }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                //    
                //    Close();
                //}
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }
        #endregion
        public void Refresh(IBL.BL accseccBL1,DroneToList drone)//מרענן את הדף
        {

            LabelErrorTime.Visibility = Visibility.Hidden;
            GridAddDrone.Visibility = Visibility.Hidden;//עדכון מופעל
            comoboxTime.Visibility = Visibility.Hidden;
            LabelTime.Visibility = Visibility.Hidden;
            LabelErrorMode.Visibility = Visibility.Hidden;
            accseccBL2 = accseccBL1;
            droneTo = drone;
            LabelId2.Content = Convert.ToString(droneTo.Id);
            LabelWeight2.Content = droneTo.Weight;//משקל
            LabeLocation2.Content = droneTo.LocationDrone;//מיקופ
            TexBoxModel.Text = droneTo.Model;//מודל
                                             //אם יש שינוי

            if (droneTo.StatusDrone == Enums.StatusDrone.InMaintenance)//אם בתחזוקה אז יש אפשרות לשחחרר רחםן בטעינה
            {
                statuse.Content = " הרחפן בטעינה";
                BottonToFun.Content = "Release from charging";
                comoboxTime.Visibility = Visibility.Visible;
                LabelTime.Visibility = Visibility.Visible;
                TimeSpan[] a = { new TimeSpan(1, 30, 0), new TimeSpan(2, 30, 0), new TimeSpan(4, 0, 0), new TimeSpan(5, 0, 0) };
                comoboxTime.ItemsSource = a;
                BottonToFun2.Visibility = Visibility.Hidden;
            }
                
            if (droneTo.StatusDrone == Enums.StatusDrone.available)//אם זמין אז יש אפשרות או לשייך חבילה או לשלוח לטעינה
            {
                BottonToFun2.Visibility = Visibility.Visible;
                BottonToFun.Visibility = Visibility.Visible;
                statuse.Content = " הרחפן  זמין";
                BottonToFun.Content = "Send for loading";
                BottonToFun2.Content = "Assignment to the package";
            }
                
            if (droneTo.StatusDrone == Enums.StatusDrone.delivered)//אם בהבלה
            {
                int a = droneTo.IdParcel;
                if (a != 0)
                {
                    Enums.StatusParcel pp = accseccBL2.StatuseParcelKnow(a);

                    if (pp == Enums.StatusParcel.collected)//זה נאסף נישאר לספק
                    {
                        statuse.Content = "הרחפן אסף את החבילה";
                        BottonToFun2.Content = "Provide package";
                        BottonToFun2.Visibility = Visibility.Visible;
                    }
                    if (pp == Enums.StatusParcel.associated)//זה שוייך צריך לאסוף
                    {
                        statuse.Content = " הרחפן  שוייך לחבילה";
                        BottonToFun.Visibility = Visibility.Visible;
                        BottonToFun.Content = "Collect a package";
                    }
                    
                }

            }
            TexBattery.Text = droneTo.StatusBatter.ToString() + "%";//בטריה
                                                                    // TexBoxModel.IsReadOnly = true;//טקסט מודל רק לקריאה
            LinearGradientBrush myBrush = new();//צבע בטריה                      
            TexBattery.Background = myBrush;
            if (droneTo.StatusBatter < 21)
            {
                myBrush.GradientStops.Add(new GradientStop(Colors.Red, 1.0));
                TexBattery.Background = myBrush;
            }

            if ((droneTo.StatusBatter > 21) & (droneTo.StatusBatter < 80))
            {
                myBrush.GradientStops.Add(new GradientStop(Colors.Orange, 0.5));
                TexBattery.Background = myBrush;
            }

            if (droneTo.StatusBatter < 101 & droneTo.StatusBatter > 80)
            {
                myBrush.GradientStops.Add(new GradientStop(Colors.Green, 0.0));
                TexBattery.Background = myBrush;
            }

        }

        public DronWindow(IBL.BL accseccBL1, DroneToList drone,DroneListWindow droneList)//update
        {
            InitializeComponent();
            GridAddDrone.Visibility = Visibility.Hidden;//עדכון מופעל
            GridUpDrone.Visibility = Visibility.Visible;
            droneListWindow11 = droneList;//מקבל את החלון רשימת רחפן כשי שיוכל לעדכן ברענון
            LabelErrorTime.Visibility = Visibility.Hidden; 
            comoboxTime.Visibility = Visibility.Hidden;
            LabelTime.Visibility = Visibility.Hidden;
            LabelErrorMode.Visibility = Visibility.Hidden;
            accseccBL2 = accseccBL1;
            droneTo = drone;
            LabelId2.Content = Convert.ToString(droneTo.Id);
            LabelWeight2.Content = droneTo.Weight;//משקל
            LabeLocation2.Content = droneTo.LocationDrone;//מיקופ
            TexBoxModel.Text = droneTo.Model;//מודל
                                             //אם יש שינוי

            if (droneTo.StatusDrone == Enums.StatusDrone.InMaintenance)//אם בתחזוקה אז יש אפשרות לשחחרר רחםן בטעינה
            {
                statuse.Content = " הרחפן בטעינה";
                BottonToFun.Content = "Release from charging";
                comoboxTime.Visibility = Visibility.Visible;
                LabelTime.Visibility = Visibility.Visible;
                TimeSpan[] a = { new TimeSpan(1, 30, 0), new TimeSpan(2, 30, 0), new TimeSpan(4, 0, 0), new TimeSpan(5, 0, 0) };
                comoboxTime.ItemsSource = a;
                BottonToFun2.Visibility = Visibility.Hidden;
            }

            if (droneTo.StatusDrone == Enums.StatusDrone.available)//אם זמין אז יש אפשרות או לשייך חבילה או לשלוח לטעינה
            {
                BottonToFun2.Visibility = Visibility.Visible;
                BottonToFun.Visibility = Visibility.Visible;
                statuse.Content = " הרחפן  זמין";
                BottonToFun.Content = "Send for loading";
                BottonToFun2.Content = "Assignment to the package";
            }

            if (droneTo.StatusDrone == Enums.StatusDrone.delivered)//אם בהבלה
            {
                int a = droneTo.IdParcel;
                if (a != 0)
                {
                    Enums.StatusParcel pp = accseccBL2.StatuseParcelKnow(a);

                    if (pp == Enums.StatusParcel.collected)//זה נאסף נישאר לספק
                    {
                        statuse.Content = "הרחפן אסף את החבילה";
                        BottonToFun2.Content = "Provide package";
                        BottonToFun2.Visibility = Visibility.Visible;
                    }
                    if (pp == Enums.StatusParcel.associated)//זה שוייך צריך לאסוף
                    {
                        statuse.Content = " הרחפן  שוייך לחבילה";
                        BottonToFun.Visibility = Visibility.Visible;
                        BottonToFun.Content = "Collect a package";
                    }

                }

            }
            TexBattery.Text = droneTo.StatusBatter.ToString() + "%";//בטריה                                                         // TexBoxModel.IsReadOnly = true;//טקסט מודל רק לקריאה
            LinearGradientBrush myBrush = new();//צבע בטריה                      
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
            bool flag = false;
            if ((string)BottonToFun.Content == "Release from charging")//שחרור רחפן מטעינה אם הרחפן לא זמין
            {
                if (comoboxTime.SelectedItem != null)
                {
                    TimeSpan s =  (TimeSpan)comoboxTime.SelectedItem;
                    try
                    {
                        flag = true;
                        accseccBL2.ReleaseDrone(droneTo.Id, s);
                        MessageBox.Show("Drone: "+droneTo.Id+ " Release from charging"); 
                        droneListWindow11.DronesListView.ItemsSource = accseccBL2.GetDrons();
                        Refresh(accseccBL2, accseccBL2.DroneToLisToPrint(droneTo.Id));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                        
                    }
                }
                else
                {
                    LabelErrorTime.Visibility = Visibility.Visible;
                }


            }
            if ((string)BottonToFun.Content == "Send for loading"&&flag==false)//שליחה לטעינה רק אם הרחפן זמין
            {
                try
                {
                    accseccBL2.SendingDroneToCharging(droneTo.Id);
                    MessageBox.Show("Drone: "+ droneTo.Id+ " Send for loading");
                    droneListWindow11.DronesListView.ItemsSource = accseccBL2.GetDrons();
                    Refresh(accseccBL2, accseccBL2.DroneToLisToPrint(droneTo.Id));
                }
   
                 catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                    
                }
              

            }
            if ((string)BottonToFun.Content == "Collect a package")//לאסוף תחבילה רק אם הרחפן בעובלה והוא שוייך לחבילה
            {
                try
                {
                    accseccBL2.PickUpPackage(droneTo.Id);
                    MessageBox.Show("Drone: " + droneTo.Id + " Collect a package: " + droneTo.IdParcel);
                    droneListWindow11.DronesListView.ItemsSource = accseccBL2.GetDrons();
                    Refresh(accseccBL2, accseccBL2.DroneToLisToPrint(droneTo.Id));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                    
                }
                
            }
        }


        private void BottonToFun2_Click(object sender, RoutedEventArgs e)
        {

            if ((string)BottonToFun2.Content == "Assignment to the package")//שיוך חבילה לרחפן רק אם הרחפן זמין
            {
                try
                {
                    accseccBL2.AssignPackageToDrone(droneTo.Id);
                    MessageBox.Show("Drone: " + droneTo.Id + " Assignment to the package: "+ droneTo.IdParcel);
                    droneListWindow11.DronesListView.ItemsSource = accseccBL2.GetDrons();
                    Refresh(accseccBL2, accseccBL2.DroneToLisToPrint(droneTo.Id));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                    
                }
               
                
            }

            if ((string)BottonToFun2.Content == "Provide package")
            {
                try
                {
                    accseccBL2.PackageDeliveryByDrone(droneTo.Id);//סיפוק חבילה רק אם הרחפן בעובלה והוא אסף תחבילה
                    MessageBox.Show("Drone: " + droneTo.Id + " Provide package: " + droneTo.IdParcel);
                    droneListWindow11.DronesListView.ItemsSource = accseccBL2.GetDrons();
                    Refresh(accseccBL2, accseccBL2.DroneToLisToPrint(droneTo.Id));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                    
                }                  
            }
          
        }
        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
           
            if (TexBoxModel.Text != "")
            {
                LabelErrorMode.Visibility = Visibility.Hidden;
                string modelnew = TexBoxModel.Text;
                try
                {
                    accseccBL2.UpdateDrone(droneTo.Id, modelnew);
                    MessageBox.Show("update Model of drone: "+ droneTo.Id+ " is Succeeded");
                    droneListWindow11.DronesListView.ItemsSource = accseccBL2.GetDrons();
                    Refresh(accseccBL2, accseccBL2.DroneToLisToPrint(droneTo.Id));
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
            Close();
        }
    }
}
