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
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : UserControl
    {
        HRSFACTORY hrs;
        public Admin()
        {
            InitializeComponent();
             hrs = new HRSFACTORY();
        }

        private void Click_cancle(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MenuMain());
        }

        private void Btn_Add(object sender, RoutedEventArgs e)
        {
            
          
            String nam=user_name.Text;
            String lnam=lname.Text;
            String cnic_no=cnic.Text;
            String cell=ph_no.Text;
            String address=add.Text;
            String paswrd = pass.Text;
            #region validation
            bool valid = true;
            if (nam.Trim() == "")
            {
                user_name.BorderBrush = Brushes.Red;
                user_name.BorderThickness = new Thickness(2, 2, 2, 2);
                valid = false;
                //MessageBox.Show("Name should not be Empty","Enter add name",MessageBoxButton.OK, MessageBoxImage.Stop);
                //return;

            }
            if (!cnic.IsMaskCompleted)
            {
                cnic.BorderBrush = Brushes.Red;
                cnic.BorderThickness = new Thickness(2, 2, 2, 2);
                valid = false;
                // MessageBox.Show("Name should not be Empty", "Enter add name", MessageBoxButton.OK, MessageBoxImage.Stop);
                //return;

            }

            if (paswrd == "")
            {

                pass.BorderBrush = Brushes.Red;
                pass.BorderThickness = new Thickness(2, 2, 2, 2);
                valid = false;
                

            }
            if (lnam.Trim() == "")
            {

                lname.BorderBrush = Brushes.Red;
                lname.BorderThickness = new Thickness(2, 2, 2, 2);
                valid = false;
                // MessageBox.Show("Name should not be Empty", "Enter add name", MessageBoxButton.OK, MessageBoxImage.Stop);
                //return;

            }
            if (address.Trim() == "")
            {

                add.BorderBrush = Brushes.Red;
                add.BorderThickness = new Thickness(2, 2, 2, 2);
                valid = false;
                // MessageBox.Show("Name should not be Empty", "Enter add name", MessageBoxButton.OK, MessageBoxImage.Stop);
                //return;

            }
            if (!ph_no.IsMaskCompleted)
            {
                ph_no.BorderBrush = Brushes.Red;
                ph_no.BorderThickness = new Thickness(2, 2, 2, 2);
                valid = false;
                // MessageBox.Show("Name should not be Empty", "Enter add name", MessageBoxButton.OK, MessageBoxImage.Stop);
                //return;

            }
            

            if (!valid)
            {
                MessageBox.Show("All field are required", "information", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            #endregion

           

            user u = new user();
            u.user_name = nam;
            u.last_name = lnam;
            u.password = paswrd;
            u.cell_no = cell;
            u.Address = address;
            u.cnic_ = cnic_no;
            hrs.adduser( u);
            this.user_name.Text = null;
            this.lname.Text = null;
            this.cnic.Text = null;
            this.add.Text = null;
            this.pass.Text = null;
            this.ph_no.Text = null;
            MessageBox.Show("User_SAVED");
        }
        

        private void Btn_Delete(object sender, RoutedEventArgs e)
        {
            String nam = dnam.Text;
            String cnic = dcnic.Text;
            #region validation

            bool valid = true;
            if (nam.Trim() == "")
            {

                dnam.BorderBrush = Brushes.Red;
                dnam.BorderThickness = new Thickness(2, 2, 2, 2);
                valid = false;
            }
            if (!dcnic.IsMaskCompleted)
            {


                dcnic.BorderBrush = Brushes.Red;
                dcnic.BorderThickness = new Thickness(2, 2, 2, 2);
                valid = false;
                

            }


            if (!valid)
            {
                MessageBox.Show("All field are required", "information", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            #endregion

            user dur= new user();
            dur.user_name = nam;
            dur.cnic_ = cnic;
            hrs.deleteuser(dur);
            dnam.Text = null;
            dcnic.Text = null;
            MessageBox.Show("User_DELETED");
            
        }

        private void addrooms(object sender, RoutedEventArgs e)
        {
            room rm= new room();
            String rmtype = type_room.Text;
            String rm_no = add_room.Text;
            #region validation
            bool valid = true;
            if (rmtype == "select room_type")
            {

                type_room.BorderBrush = Brushes.Red;
                type_room.BorderThickness = new Thickness(2, 2, 2, 2);
                valid = false;
            }
          
             if (rm_no.Trim() == "")
            {

                add_room.BorderBrush = Brushes.Red;
                add_room.BorderThickness = new Thickness(2, 2, 2, 2);
                valid = false;
            }

            if (!valid)
            {
                MessageBox.Show("All field are required", "information", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            #endregion
            
            rm.room_type = rmtype;
            rm.status = "unreserved";
            if (rm.room_type == "single bed") 
            {
                
                int rnt = 5000;
                rm.rent = rnt;
                hrs.addroom(rm,int.Parse(rm_no));
            }
            else if(rm.room_type == "double bed")
            {
            int rnt = 9000;
                rm.rent = rnt;
                hrs.addroom(rm,int.Parse(rm_no));
            }
            else
            {
            MessageBox.Show("invalid","room type not selected",MessageBoxButton.OK,MessageBoxImage.Stop);
            }
            type_room.Text = "select room_type";
            add_room.Text = null;
            MessageBox.Show("ROOMS_ADDED");    
        }
        #region validation
        private void add_room_TextChanged(object sender, TextChangedEventArgs e)
        {
            add_room.BorderBrush = Brushes.White;
        }
        #endregion

        private void dnam_TextChanged(object sender, TextChangedEventArgs e)
        {
            dnam.BorderBrush = Brushes.White;
        }

        private void dcnic_TextChanged(object sender, TextChangedEventArgs e)
        {
            dcnic.BorderBrush = Brushes.White;
        }

        private void user_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            user_name.BorderBrush = Brushes.White;
        }

        private void lname_TextChanged(object sender, TextChangedEventArgs e)
        {
            lname.BorderBrush = Brushes.White;
        }

        private void cnic_TextChanged(object sender, TextChangedEventArgs e)
        {
            cnic.BorderBrush = Brushes.White;
        }

        private void ph_no_TextChanged(object sender, TextChangedEventArgs e)
        {
            ph_no.BorderBrush = Brushes.White;
        }

        private void add_TextChanged(object sender, TextChangedEventArgs e)
        {
            add.BorderBrush = Brushes.White;
        }

        private void pass_TextChanged(object sender, TextChangedEventArgs e)
        {
            pass.BorderBrush = Brushes.White;
        }




    }
    }
