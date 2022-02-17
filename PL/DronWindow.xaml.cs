﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
using BO;
using System.Text.RegularExpressions;
using BlApi;
using System.Diagnostics;
using System.Threading;

namespace PL
{
    /// <summary>
    /// Interaction logic for DronWindow.xaml
    /// </summary>
   
    public partial class DronWindow : Window
    {
        private readonly DroneListWindow droneListWindow11;
        BO.DroneToList droneTo;
        IBL accseccBL2;
        BO.Station station1;
       
        #region Add
        public DronWindow(IBL accseccBL1,DroneListWindow dd)//add
        {
           
            InitializeComponent();
            Width = 500;
            GridUpDrone.Visibility = Visibility.Hidden;//עדכון מופעל
            droneListWindow11 = dd;//מקבל חלון של דרון ליסט
            accseccBL2 = accseccBL1;
            ComboBoxWeight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
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
                if (accseccBL2.GetDrons().Any(p => p.Id == Convert.ToInt32(TextId.Text)))//קיים
                {
                    Label5.Visibility = Visibility.Visible;
                }
                else
                    Label5.Visibility = Visibility.Hidden;
            }

            if (TextId.Text != "")
            {
                if ((TextId.Text != "") & (ComboBoxWeight.SelectedItem != null) & (ComboBoxStation.SelectedItem != null) & (TexModel.Text != "") & (accseccBL2.GetDrons().Any(p => p.Id == Convert.ToInt32(TextId.Text)) == false))
                {
                    BO.Drone drone1;
                    //bool dds = accseccBL2.BlDrone.Any(p => p.Id == Convert.ToInt32(TextId.Text));
                    //try
                    //{ 
                    drone1 = new() { Id = Convert.ToInt32(TextId.Text), Model = TexModel.Text, Weight = (BO.WeightCategories)ComboBoxWeight.SelectedItem };
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
            var a = MessageBox.Show("You're sure you want to close", "closr", MessageBoxButton.YesNo);

            if (a == MessageBoxResult.Yes)
            {
                Close();
            }
        }
        #endregion
        public void Refresh(IBL accseccBL1, BO.DroneToList drone)//מרענן את הדף
        {
            ButtonParcel.Visibility = Visibility.Hidden;
            atu.Visibility = Visibility.Visible;
            GridAddDrone.Visibility = Visibility.Hidden;//עדכון מופעל
            GridUpDrone.Visibility = Visibility.Visible; 
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

            if (droneTo.StatusDrone == BO.StatusDrone.InMaintenance)//אם בתחזוקה אז יש אפשרות לשחחרר רחםן בטעינה
            {
                statuse.Content = "The Drone is charging";
                BottonToFun.Content = "Release from charging";
                comoboxTime.Visibility = Visibility.Visible;
                LabelTime.Visibility = Visibility.Visible;
                TimeSpan[] a = { new TimeSpan(1, 30, 0), new TimeSpan(2, 30, 0), new TimeSpan(4, 0, 0), new TimeSpan(5, 0, 0) };
                comoboxTime.ItemsSource = a;
                BottonToFun2.Visibility = Visibility.Hidden;

                station1 = accseccBL2.GetStationByDrone(droneTo.Id);

            }

            if (droneTo.StatusDrone == BO.StatusDrone.available)//אם זמין אז יש אפשרות או לשייך חבילה או לשלוח לטעינה
            {
                BottonToFun2.Visibility = Visibility.Visible;
                BottonToFun.Visibility = Visibility.Visible;
                statuse.Content = "The Drone is available";
                BottonToFun.Content = "Send for loading";
                BottonToFun2.Content = "Assignment to the package";
            }

            if (droneTo.StatusDrone == BO.StatusDrone.delivered)//אם בהבלה
            {
                int a = droneTo.IdParcel;
                if (a != 0)
                {
                    BO.StatusParcel pp = accseccBL2.StatuseParcelKnow(a);


                    if (pp == BO.StatusParcel.collected)//זה נאסף נישאר לספק
                    {
                        statuse.Content = "The Drone collected the package";
                        BottonToFun2.Content = "Provide package";
                        BottonToFun2.Visibility = Visibility.Visible;//רק לספק
                        BottonToFun.Visibility = Visibility.Hidden;
                        ButtonParcel.Visibility = Visibility.Visible;
                        ButtonParcel.Content = "Click to see the package number: " + a;
                    }
                    if (pp == BO.StatusParcel.associated)//זה שוייך צריך לאסוף
                    {
                        statuse.Content = "The Drone belongs to the package";
                        BottonToFun.Visibility = Visibility.Visible;//רק לאסוף
                        BottonToFun.Content = "Collect a package";
                        BottonToFun2.Visibility = Visibility.Hidden;
                        ButtonParcel.Visibility = Visibility.Visible;
                        ButtonParcel.Content = "TClick to see the package number: " + a;
                    }

                }

            }
            TexBattery.Text = Convert.ToInt32(droneTo.StatusBatter).ToString() + "%";//בטריה                                                          
            LinearGradientBrush myBrush = new();//צבע בטריה                      
            TexBattery.Background = myBrush;
            if (droneTo.StatusBatter < 21)
            {
                myBrush.GradientStops.Add(new GradientStop(Colors.Red, 1.0));
                TexBattery.Background = myBrush;
                //TexBattery
            }

            if ((droneTo.StatusBatter > 20) & (droneTo.StatusBatter < 81))
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

        public DronWindow(IBL accseccBL1,BO.DroneToList drone,DroneListWindow droneList)//update
        {
            InitializeComponent();
            worker = new BackgroundWorker();
            
            ButtonParcel.Visibility = Visibility.Hidden;
            atu.Visibility = Visibility.Visible;
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

            if (droneTo.StatusDrone == BO.StatusDrone.InMaintenance)//אם בתחזוקה אז יש אפשרות לשחחרר רחםן בטעינה
            {
                statuse.Content = "The Drone is charging";
                BottonToFun.Content = "Release from charging";
                comoboxTime.Visibility = Visibility.Visible;
                LabelTime.Visibility = Visibility.Visible;
                TimeSpan[] a = { new TimeSpan(1, 30, 0), new TimeSpan(2, 30, 0), new TimeSpan(4, 0, 0), new TimeSpan(5, 0, 0) };
                comoboxTime.ItemsSource = a;
                BottonToFun2.Visibility = Visibility.Hidden;
                BottonToFun.Visibility = Visibility.Visible;
                station1 = accseccBL2.GetStationByDrone(droneTo.Id);

            }

            if (droneTo.StatusDrone == BO.StatusDrone.available)//אם זמין אז יש אפשרות או לשייך חבילה או לשלוח לטעינה
            {
                BottonToFun2.Visibility = Visibility.Visible;
                BottonToFun.Visibility = Visibility.Visible;
                statuse.Content = "The Drone is available";
                BottonToFun.Content = "Send for loading";
                BottonToFun2.Content = "Assignment to the package";
            }

            if (droneTo.StatusDrone == BO.StatusDrone.delivered)//אם בהבלה
            {
                int a = droneTo.IdParcel;
                if (a != 0)
                {
                    BO.StatusParcel pp = accseccBL2.StatuseParcelKnow(a);

                    if (pp == BO.StatusParcel.collected)//זה נאסף נישאר לספק
                    {
                        statuse.Content = "The Drone collected the package";
                        BottonToFun2.Content = "Provide package";
                        BottonToFun2.Visibility = Visibility.Visible;//רק לספק
                        BottonToFun.Visibility = Visibility.Hidden;
                        ButtonParcel.Visibility = Visibility.Visible;
                        ButtonParcel.Content = "Click to see the package number: " + a;
                    }
                    if (pp == BO.StatusParcel.associated)//זה שוייך צריך לאסוף
                    {
                        statuse.Content = "The Drone belongs to the package";
                        BottonToFun.Visibility = Visibility.Visible;//רק לאסוף
                        BottonToFun.Content = "Collect a package";
                        BottonToFun2.Visibility = Visibility.Hidden;
                        ButtonParcel.Visibility = Visibility.Visible;
                        ButtonParcel.Content = "Click to see the package number: " + a;
                    }

                }

            }
            TexBattery.Text = Convert.ToInt32(droneTo.StatusBatter).ToString() + "%";//בטריה                                                         
            LinearGradientBrush myBrush = new();//צבע בטריה                      
            TexBattery.Background = myBrush;
            if (droneTo.StatusBatter < 21)
            {
                myBrush.GradientStops.Add(new GradientStop(Colors.Red, 1.0));
                TexBattery.Background = myBrush;
                //TexBattery
            }

            if ((droneTo.StatusBatter > 20) & (droneTo.StatusBatter < 81))
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
        public DronWindow(IBL accseccBL1, BO.DroneToList drone)// מקבל את הרחפן מחלון של תחנה או מחלון חבילה
        {
            InitializeComponent();
            ButtonParcel.Visibility = Visibility.Hidden;
            GridAddDrone.Visibility = Visibility.Hidden;//עדכון מופעל
            GridUpDrone.Visibility = Visibility.Visible;
            atu.Visibility = Visibility.Hidden;
            LabelErrorTime.Visibility = Visibility.Hidden;
            comoboxTime.Visibility = Visibility.Hidden;
            LabelTime.Visibility = Visibility.Hidden;
            LabelErrorMode.Visibility = Visibility.Hidden;
            accseccBL2 = accseccBL1;
            droneTo = drone;
            ButtonUpdate.Visibility = Visibility.Hidden;
            TexBoxModel.IsReadOnly = true;
            LabelId2.Content = Convert.ToString(droneTo.Id);
            LabelWeight2.Content = droneTo.Weight;//משקל
            LabeLocation2.Content = droneTo.LocationDrone;//מיקופ
            TexBoxModel.Text = droneTo.Model;//מודל
            BottonToFun2.Visibility = Visibility.Hidden;
            BottonToFun.Visibility = Visibility.Hidden;                             //אם יש שינוי
            ButtonParcel.Visibility = Visibility.Hidden;
            if (droneTo.StatusDrone == BO.StatusDrone.InMaintenance)//אם בתחזוקה אז יש אפשרות לשחחרר רחםן בטעינה
            {
                statuse.Content = "The Drone is charging";
               }

            if (droneTo.StatusDrone == BO.StatusDrone.available)//אם זמין אז יש אפשרות או לשייך חבילה או לשלוח לטעינה
            {
               
                statuse.Content = "The Drone is available";
                
            }

            if (droneTo.StatusDrone == BO.StatusDrone.delivered)//אם בהבלה
            {
                int a = droneTo.IdParcel;
                if (a != 0)
                {
                    BO.StatusParcel pp = accseccBL2.StatuseParcelKnow(a);

                    if (pp == BO.StatusParcel.collected)//זה נאסף נישאר לספק
                    {
                        statuse.Content = "The Drone collected the package";
                       
                    }
                    if (pp == BO.StatusParcel.associated)//זה שוייך צריך לאסוף
                    {
                        statuse.Content = "The Drone belongs to the package";
                        
                    }

                }

            }
            TexBattery.Text = Convert.ToInt32(droneTo.StatusBatter).ToString() + "%";//בטריה                                                         
            LinearGradientBrush myBrush = new();//צבע בטריה                      
            TexBattery.Background = myBrush;
            if (droneTo.StatusBatter < 21)
            {
                myBrush.GradientStops.Add(new GradientStop(Colors.Red, 1.0));
                TexBattery.Background = myBrush;
                //TexBattery
            }

            if ((droneTo.StatusBatter > 20) & (droneTo.StatusBatter < 81))
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
                    
                    accseccBL2.PickUpPackage(accseccBL2.GetDrone(droneTo.Id).Id);
                    droneListWindow11.DronesListView.ItemsSource = accseccBL2.GetDrons();
                    MessageBox.Show("Drone: " + droneTo.Id + " Collect a package: " + droneTo.IdParcel);
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
                   
                    droneListWindow11.DronesListView.ItemsSource = accseccBL2.GetDrons();
                    MessageBox.Show("Drone: " + droneTo.Id + " Assignment to the package: " + accseccBL2.GetDrone(droneTo.Id).PackageInTransfe.Id);
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
                    accseccBL2.PackageDeliveryByDrone(accseccBL2.GetDrone(droneTo.Id).Id);//סיפוק חבילה רק אם הרחפן בעובלה והוא אסף תחבילה
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
            var a = MessageBox.Show("You're sure you want to close", "closr", MessageBoxButton.YesNo);

            if (a == MessageBoxResult.Yes)
            {
                worker?.CancelAsync();
                Close();
            }
        }

        private void ButtonParcel_Click(object sender, RoutedEventArgs e)
        {
            BO.ParcelToLIst pp = accseccBL2.ParcelToListToPrint(droneTo.IdParcel);
            ParcelWindow par = new ParcelWindow(accseccBL2, pp);
            par.Show();
        }




        bool autBool = false;
        BackgroundWorker worker;
        private void updateDrone() => worker.ReportProgress(0);
        private bool checkStop() => worker.CancellationPending;

        private void atu_Click(object sender, RoutedEventArgs e)
        {

            //צריך להתחיל את התעליכון כי לחצתי על אוטומטי
            if (worker.WorkerSupportsCancellation == true)
            {
                //מכבים את התעליכון
                atu.Content = "automatic";
                autBool = false;
                BottonToFun.IsEnabled = true;
                BottonToFun2.IsEnabled = true;
                ButtonUpdate.Visibility = Visibility.Visible;
                TexBoxModel.IsReadOnly = false;
                DroneToList droneToList = accseccBL2.DroneToLisToPrint(droneTo.Id);
                Refresh(accseccBL2, droneToList);
                worker?.CancelAsync();
            }
            else//
            if (worker.IsBusy != true)
            {
                {
                    //מדליקים
                    BottonToFun.IsEnabled = false;
                    BottonToFun2.IsEnabled = false;
                    ButtonUpdate.Visibility = Visibility.Hidden;
                    TexBoxModel.IsReadOnly = true;
                    atu.Content = "manual";
                    autBool = true;
                    worker = new BackgroundWorker()
                    {
                        WorkerReportsProgress = true,
                        WorkerSupportsCancellation = true,
                    };
                    worker.DoWork += (sender, args) => accseccBL2.StartDroneSimulator(droneTo.Id, updateDrone, checkStop);//simulator
                    worker.RunWorkerCompleted += (sender, args) => {
                        SimulatorIsActive.IsChecked = false;
                        worker = null;
                        // if (enter.Visibility == Visibility.Hidden) Close();
                    };
                    worker.ProgressChanged += (sender, args) => updateDroneView();
                    worker.RunWorkerAsync(droneTo.Id);
                    worker.DoWork += Worker_DoWork;
                    worker.ProgressChanged += Worker_ProgressChanged;
                    worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                    worker.RunWorkerAsync(35);
                }
            }

        }


        private void updateDroneView()
        {
            
            LabeLocation2.Content = droneTo.LocationDrone;//מיקופ

            if (droneTo.StatusDrone == BO.StatusDrone.InMaintenance)//אם בתחזוקה אז יש אפשרות לשחחרר רחםן בטעינה
                statuse.Content = "The Drone is charging";

            if (droneTo.StatusDrone == BO.StatusDrone.available)//אם זמין אז יש אפשרות או לשייך חבילה או לשלוח לטעינה 
                statuse.Content = "The Drone is available";
               
            if (droneTo.StatusDrone == BO.StatusDrone.delivered)//אם בהבלה
            {
                ButtonParcel.Content = "the parcel is: " + droneTo.IdParcel;
                int a = droneTo.IdParcel;
                if (a != 0)
                {
                    BO.StatusParcel pp = accseccBL2.StatuseParcelKnow(a);

                    if (pp == BO.StatusParcel.collected)//זה נאסף נישאר לספק
                        statuse.Content = "The Drone collected the package";
                    if (pp == BO.StatusParcel.associated)//זה שוייך צריך לאסוף
                        statuse.Content = "The Drone belongs to the package"; 
                   

                }

            }
            TexBattery.Text = Convert.ToInt32(droneTo.StatusBatter).ToString() + "%";//בטריה                                                          
            LinearGradientBrush myBrush = new();//צבע בטריה                      
            TexBattery.Background = myBrush;
            if (droneTo.StatusBatter < 21)
            {
                myBrush.GradientStops.Add(new GradientStop(Colors.Red, 1.0));
                TexBattery.Background = myBrush;
                //TexBattery
            }

            if ((droneTo.StatusBatter > 20) & (droneTo.StatusBatter < 81))
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
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

             BackgroundWorker worker = sender as BackgroundWorker;

            int length = (int)e.Argument;

            for (int i = 1; i <= length; i++)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    Thread.Sleep(500);
                    if (worker.WorkerReportsProgress == true)
                        worker.ReportProgress(i * 100 / length);
                }
            }
            e.Result = stopwatch.ElapsedMilliseconds;
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            TexBattery.Text = progress + "%";
            resultProgressBar.Value = progress;
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                TexBattery.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                TexBattery.Text = "Error: " + e.Error.Message; // Exception Message
            }
            else
            {
                long result = (long)e.Result;
                if (result < 1000)
                    TexBattery.Text = "Done after " + result + " ms.";
                else
                    TexBattery.Text = "Done after " + result / 1000 + " sec.";
            }
        }
    }
}
