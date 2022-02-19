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
        bool customer = false;
        public ParcelListWindow(IBL accseccBL,bool cur=false)
        {
            InitializeComponent();
            accseccBL1 = accseccBL;
            customer = cur;
            parcelToLIstDataGrid.DataContext= accseccBL1.GetParcels();
            parcelToLIstDataGrid.IsReadOnly = true;
            senderrr.ItemsSource = accseccBL1.GetCustomersName();
            target.ItemsSource = accseccBL1.GetCustomersName();
            statusPaket.ItemsSource= Enum.GetValues(typeof(BO.StatusParcel));
        }

       
        private void AddParcel_Click(object sender, RoutedEventArgs e)
        {
            ParcelWindow dr = new ParcelWindow(accseccBL1, this);//מקבל גם גישה וגם את החלון כדי שיוכל לסגור אותו
            dr.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (customer == true)
            {
                ListsWindow we = new ListsWindow(accseccBL1, true);
                we.Show();
                Close();
            }
            else
            {
                ListsWindow we = new ListsWindow(accseccBL1);
                we.Show();
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

       

        private void parcelToLIstDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ParcelToLIst parcel = parcelToLIstDataGrid.SelectedItem as BO.ParcelToLIst;
            if (parcel != null)
            {
                ParcelWindow dr = new ParcelWindow(accseccBL1, parcel, this);
                dr.Show();
            }
            
        }
        private void comoboxByParcel()//סינון החבילה לפי השולח והמקבל
        {
            if (senderrr.SelectedItem == null && target.SelectedItem == null&&statusPaket==null)
            {
                parcelToLIstDataGrid.DataContext = accseccBL1.GetParcels();//ממלא את הרשימה
            }
            if (target.SelectedIndex != -1 && senderrr.SelectedIndex != -1 && statusPaket.SelectedIndex != -1)//by sender,tatget,status
                parcelToLIstDataGrid.DataContext = accseccBL1.GetParcelByPerdicate(x => x.SenderName == Convert.ToString(senderrr.SelectedItem) && x.TargetName == Convert.ToString(target.SelectedItem) && (int)x.statusParcel == Convert.ToInt32(statusPaket.SelectedItem));
            else if (target.SelectedIndex != -1 && senderrr.SelectedIndex != -1 && statusPaket.SelectedIndex == -1)
                parcelToLIstDataGrid.DataContext = accseccBL1.GetParcelByPerdicate(x => x.SenderName == Convert.ToString(senderrr.SelectedItem) && x.TargetName == Convert.ToString(target.SelectedItem));
            else if (target.SelectedIndex == -1 && senderrr.SelectedIndex != -1 && statusPaket.SelectedIndex == -1)//by sender
                parcelToLIstDataGrid.DataContext = accseccBL1.GetParcelByPerdicate(x => x.SenderName == Convert.ToString(senderrr.SelectedItem));
            else if (target.SelectedIndex != -1 && senderrr.SelectedIndex == -1 && statusPaket.SelectedIndex == -1)//by target
                parcelToLIstDataGrid.DataContext = accseccBL1.GetParcelByPerdicate(x => x.TargetName == Convert.ToString(target.SelectedItem));
            else if (target.SelectedIndex == -1 && senderrr.SelectedIndex != -1 && statusPaket.SelectedIndex != -1)//by sender,atatus
                parcelToLIstDataGrid.DataContext = accseccBL1.GetParcelByPerdicate(x => x.SenderName == Convert.ToString(senderrr.SelectedItem) && (int)x.statusParcel == Convert.ToInt32(statusPaket.SelectedItem));
            else if (target.SelectedIndex != -1 && senderrr.SelectedIndex == -1 && statusPaket.SelectedIndex != -1)//by target and status
                parcelToLIstDataGrid.DataContext = accseccBL1.GetParcelByPerdicate(x => x.TargetName == Convert.ToString(target.SelectedItem) && (int)x.statusParcel == Convert.ToInt32(statusPaket.SelectedItem));
            else if (target.SelectedIndex == -1 && senderrr.SelectedIndex == -1 && statusPaket.SelectedIndex != -1) //by status
                parcelToLIstDataGrid.DataContext = accseccBL1.GetParcelByPerdicate(x => (int)x.statusParcel == Convert.ToInt32(statusPaket.SelectedItem));
            else if (target.SelectedIndex == -1 && senderrr.SelectedIndex != -1 && statusPaket.SelectedIndex == -1)//by sender,target
                parcelToLIstDataGrid.DataContext = accseccBL1.GetParcelByPerdicate(x => x.SenderName == Convert.ToString(senderrr.SelectedItem) && x.TargetName == Convert.ToString(target.SelectedItem));



            //else if (target.SelectedIndex != -1 && senderrr.SelectedIndex != -1)//גם לפי מקבל ולפי שולח
            //    parcelToLIstDataGrid.DataContext = accseccBL1.GetParcelByPerdicate(x => x.SenderName == Convert.ToString(senderrr.SelectedItem) && x.TargetName == Convert.ToString(target.SelectedItem));
            //else if (target.SelectedIndex != -1 && senderrr.SelectedIndex == -1)//רק לפי מקבל
            //    parcelToLIstDataGrid.DataContext = accseccBL1.GetParcelByPerdicate(x => x.TargetName == Convert.ToString(target.SelectedItem));
            //else if (target.SelectedIndex == -1 && senderrr.SelectedIndex != -1)//רק לפי שולח
            //    parcelToLIstDataGrid.DataContext = accseccBL1.GetParcelByPerdicate(x => x.SenderName == Convert.ToString(senderrr.SelectedItem));


        }

        private void sender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comoboxByParcel();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comoboxByParcel();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//רק לפי השולח והסטטוס כי ניקינו בחירה
        {
            if (senderrr.SelectedIndex != -1 && statusPaket.SelectedIndex != -1)//by sender and status
                parcelToLIstDataGrid.DataContext = accseccBL1.GetParcelByPerdicate(x => x.SenderName == Convert.ToString(senderrr.SelectedItem) && (int)x.statusParcel == Convert.ToInt32(statusPaket.SelectedItem));
            else if (senderrr.SelectedIndex == -1 && statusPaket.SelectedIndex != -1)//by status
                parcelToLIstDataGrid.DataContext = accseccBL1.GetParcelByPerdicate(x => (int)x.statusParcel == Convert.ToInt32(statusPaket.SelectedItem));
            else if (senderrr.SelectedIndex != -1 && statusPaket.SelectedIndex == -1)//by sender
                parcelToLIstDataGrid.DataContext = accseccBL1.GetParcelByPerdicate(x => x.SenderName == Convert.ToString(senderrr.SelectedItem));
            else parcelToLIstDataGrid.DataContext = accseccBL1.GetParcels();//ממלא את הרשימה



            target.SelectedIndex= -1;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)//  רק לפי המקבל והסטטוס
        {
            if (target.SelectedIndex != -1 && statusPaket.SelectedIndex != -1)//by sender and status
                parcelToLIstDataGrid.DataContext = accseccBL1.GetParcelByPerdicate(x => x.TargetName == Convert.ToString(target.SelectedItem) && (int)x.statusParcel == Convert.ToInt32(statusPaket.SelectedItem));
            else if (target.SelectedIndex == -1 && statusPaket.SelectedIndex != -1)//by status
                parcelToLIstDataGrid.DataContext = accseccBL1.GetParcelByPerdicate(x => (int)x.statusParcel == Convert.ToInt32(statusPaket.SelectedItem));
            else if (target.SelectedIndex != -1 && statusPaket.SelectedIndex == -1)//by sender
                parcelToLIstDataGrid.DataContext = accseccBL1.GetParcelByPerdicate(x => x.TargetName == Convert.ToString(target.SelectedItem));
            else parcelToLIstDataGrid.DataContext = accseccBL1.GetParcels();//ממלא את הרשימה
            //if(target.SelectedIndex != -1)
            //parcelToLIstDataGrid.DataContext = accseccBL1.GetParcelByPerdicate(x => x.TargetName == Convert.ToString(target.SelectedItem));
            //else
            //    parcelToLIstDataGrid.DataContext = accseccBL1.GetParcels();//ממלא את הרשימה

            senderrr.SelectedIndex = -1;
        }

        private void statusPaket_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comoboxByParcel();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)//רק לפי השולח ומקבל
        {
            
            if (senderrr.SelectedIndex != -1 && target.SelectedIndex != -1)//by sender and target
                parcelToLIstDataGrid.DataContext = accseccBL1.GetParcelByPerdicate(x => x.SenderName == Convert.ToString(senderrr.SelectedItem) && x.TargetName == Convert.ToString(target.SelectedItem));
            else if (senderrr.SelectedIndex != -1 && target.SelectedIndex == -1)//by sender
                parcelToLIstDataGrid.DataContext = accseccBL1.GetParcelByPerdicate(x => x.SenderName == Convert.ToString(senderrr.SelectedItem));
            else if(senderrr.SelectedIndex == -1 && target.SelectedIndex != -1)//by target
                parcelToLIstDataGrid.DataContext = accseccBL1.GetParcelByPerdicate(x => x.TargetName == Convert.ToString(target.SelectedItem));
            else  parcelToLIstDataGrid.DataContext = accseccBL1.GetParcels();//ממלא את הרשימה

            statusPaket.SelectedIndex = -1;
        }
    }
}
