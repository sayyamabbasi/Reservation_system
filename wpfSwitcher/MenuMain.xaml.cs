using datalayer;
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

namespace HOTEL_RESERVATION_SYSTEM
{
    /// <summary>
    /// Interaction logic for MenuMain.xaml
    /// </summary>
    public partial class MenuMain : UserControl
    {
        HRSFACTORY hrf = new HRSFACTORY();
        public MenuMain()
        {
            InitializeComponent();
        }

        private void Click_cancle(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Login(object sender, RoutedEventArgs e)
        {
            admin adm;
            String username = user_name.Text;
            String pass = password.Password;

            if ((adm = hrf.getadmin(username, pass)) != null)
            {
                Switcher.Switch(new Admin());
            }
            else
            {

                MessageBox.Show("invalid user/pass combination","invalid user",MessageBoxButton.OK, MessageBoxImage.Stop);

            }
            user_name.Text=null;
            password.Password=null;
        }

       

        private void user_login(object sender, RoutedEventArgs e)
        {
            user usr;
                 
            String usrname = usr_name.Text;
            String usr_pass = pswrd.Password;

            if ((usr = hrf.getusr(usrname, usr_pass)) != null)
            {
                Switcher.Switch(new UserMaintainer());
           
            }
            else
            {

                MessageBox.Show("invalid user/pass combination", "invalid user", MessageBoxButton.OK, MessageBoxImage.Stop);

            }
            usr_name.Text=null;
            pswrd.Password=null;
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

       
    }
}
