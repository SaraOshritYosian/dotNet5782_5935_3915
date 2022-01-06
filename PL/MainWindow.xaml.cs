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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IEnumerable<string> passwordArr=new List<string>();
        internal readonly IBL accsessBL = BlFactory.GetBl();
        public MainWindow()
        {
            InitializeComponent();
            masengerOrCustomer.Visibility = Visibility.Visible;
            enterM.Visibility = Visibility.Hidden;
            enterC.Visibility = Visibility.Hidden;


        }

        private void Manager_Click(object sender, RoutedEventArgs e)
        {
            masengerOrCustomer.Visibility = Visibility.Hidden;
            enterM.Visibility = Visibility.Visible;
            enterC.Visibility = Visibility.Hidden;
        }

        private void Costumer_Click(object sender, RoutedEventArgs e)
        {
            masengerOrCustomer.Visibility = Visibility.Hidden;
            enterC.Visibility = Visibility.Visible;
            enterM.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)//המנהל הקיש סיסמה ורוצה לפנות לרשימות
        {
            if(passwordTextm.Text=="12345")
            {
                ListsWindow we = new ListsWindow(accsessBL);
                we.Show();
                Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//הלקוח קיים הקיש סיסמה
        {
            string a = passwordTextm.Text;
            for ( int i=0;i<passwordArr.Count();i++)
            {
                if (a == passwordArr.ElementAt(i))
                {
                    ListsWindow we = new ListsWindow(accsessBL);
                    we.Show();
                    Close();
                }
                
            }
            passwordTextm.Text="";
            MessageBox.Show("The customer does not exist in the system ");


        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//הוספת לקוח
        {
            string a = passwordTextm.Text;
            for (int i = 0; i < passwordArr.Count(); i++)
            {
                if (a == passwordArr.ElementAt(i))
                {
                    
                    MessageBox.Show("The customer exist in the system ");
                    ListsWindow wwe = new ListsWindow(accsessBL);
                    wwe.Show();
                    Close();
                }

            }
            passwordArr.ToList().Add(a);
            MessageBox.Show("Adding a new client to the system");
            ListsWindow we = new ListsWindow(accsessBL);
            we.Show();
            Close();
        }
    }
}
