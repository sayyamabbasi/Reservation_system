using System;
using datalayer;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for UserMaintainer.xaml
    /// </summary>
    public partial class UserMaintainer : UserControl
    {

        List<customer> clist;
        List<record> rlist;
        List<payment> plist;

        //List<booking> bklist;
        HRSFACTORY hrs;
        int selectedindex = -1;
        public UserMaintainer()
        {
            InitializeComponent();
            hrs = new HRSFACTORY();
            addgrid.Items.Refresh();
            update_grid.Items.Refresh();
            bill_grid.Items.Refresh();
            srcni.Visibility = Visibility.Hidden;
            srnam.Visibility = Visibility.Hidden;
            lvdate.Visibility = Visibility.Hidden;
            bkdate.Visibility = Visibility.Hidden;
            search_btn.Visibility = Visibility.Hidden;
            bkings.Visibility = Visibility.Hidden;
            rmavlbl.Visibility = Visibility.Hidden;
            rmavlbl_text.Visibility = Visibility.Hidden;
            ntavailbl.Visibility = Visibility.Hidden;
            rmavlbl_text.Visibility = Visibility.Hidden;
            next.IsEnabled = false;
            rnt.Visibility = Visibility.Hidden;
            rmsrv.Visibility = Visibility.Hidden;
            ttl.Visibility = Visibility.Hidden;
            

        }
        customer cust = null;

        private void make_booking(object sender, RoutedEventArgs e)
        {

            HRSFACTORY hrs = new HRSFACTORY();
            clist = hrs.getcustomer();
            String nam = fnam.Text;
            String lnam = lname.Text;
            String cnic_no = cnic.Text;
            String cell = ph_no.Text;
            String address = add.Text;
            String type = rm_type.Text;
            
            
            #region validation
            bool valid = true;
            if (nam.Trim() == "")
            {
                fnam.BorderBrush = Brushes.Red;
                fnam.BorderThickness = new Thickness(2, 2, 2, 2);
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

            if (type == "select room_type")
            {

                rm_type.BorderBrush = Brushes.Red;
                rm_type.BorderThickness = new Thickness(2, 2, 2, 2);
                valid = false;
                // MessageBox.Show("Name should not be Empty", "Enter add name", MessageBoxButton.OK, MessageBoxImage.Stop);
                //return;

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
            DateTime dob = new DateTime();
            dob = (DateTime)DOB.SelectedDate;
            DateTime LOD = new DateTime();
            LOD = (DateTime)lod.SelectedDate;
            System.Console.WriteLine(dob);

            if (DOB.Text == lod.Text)
            {

                MessageBox.Show("booking_date and leaving date should not be same", "information", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            
            }

            else if (DateTime.Today>dob)
            {
                MessageBox.Show("invalid booking date", "information", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            else if(dob>LOD)
            {
                MessageBox.Show("invalid leaving date", "information", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            customer cus = new customer();
            booking bk = new booking();
            bk.booking_date = dob;
            bk.leave_date = LOD;
                

            payment pay = new payment();

            cus.fname = nam;
            cus.lastname = lnam;
            cus.cnic = cnic_no;
            cus.cell_no = cell;
            cus.Address = address;



           

            TimeSpan diff = LOD - dob;
            var days = diff.Days;
            if (hrs.getcnic(cnic_no) != null)
            {
                MessageBox.Show("CNIC ALREADY", "EXIST", MessageBoxButton.OK, MessageBoxImage.Stop);

                return;
            }
            else
            {
                hrs.addcust(cus);

                hrs.addbooking(bk, cus, type);
                clist = hrs.getcustomer();
                addgrid.ItemsSource = clist;
                update_grid.ItemsSource = clist;
                addgrid.Items.Refresh();
                update_grid.Items.Refresh();
                


            }


            if (type == "single bed")
            {
                int amnt = days * 5000;
                pay.rent = amnt;
                hrs.addpayment(cus, bk, pay);

            }
            else
            {

                int rnt = days * 9000;
                pay.rent = rnt;
                hrs.addpayment(cus, bk, pay);
            }
            
            
            fnam.Text = null;
            lname.Text = null;
            cnic.Text = null;
            ph_no.Text = null;
            DOB.Text = null;
            lod.Text = null;
            add.Text = null;
            rm_type.Text = "select room_type";

            plist = hrs.getpaydata();
            bill_grid.ItemsSource = plist;
            bill_grid.Items.Refresh();

        }



        private void loadgrid(object sender, RoutedEventArgs e)
        {

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += loaddata;

            worker.RunWorkerAsync();
        }

        public void loaddata(object sender, DoWorkEventArgs e)
        {
            clist = hrs.getcustomer();
            rlist = hrs.getrecord();
            plist = hrs.getpay();
            // dlist = hrs.getcustomer();
            this.Dispatcher.Invoke(new Action(delegate
                {
                    addgrid.ItemsSource = clist;
                    update_grid.ItemsSource = clist;
                    search_grid.ItemsSource = rlist;
                    bill_grid.ItemsSource = plist;

                    //addgrid.ItemsSource = bklist;

                }));


        }


        private void update_grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.selectedindex = update_grid.SelectedIndex;
            if (selectedindex == -1 || clist.Count <= selectedindex)
            {
                cust = null;
                return;

            };
            booking bh = new booking();
            cust = (customer)update_grid.SelectedItem;
            fname.Text = cust.fname;
            clnam.Text = cust.lastname;
            ccnic.Text = cust.cnic;
            ccont.Text = cust.cell_no;
            DateTime dateb = new DateTime();
            dateb = (DateTime)cust.booking.booking_date;
            db.SelectedDate = dateb;
            ld.SelectedDate = cust.booking.leave_date;
            cadd.Text = cust.Address;
            urm_type.Text = cust.booking.room.room_type;
        }

        private void updat(object sender, RoutedEventArgs e)
        {
            String fnam = fname.Text;
            String lnam = clnam.Text;
            String cuscnic = ccnic.Text;
            String ph_no = ccont.Text;
            String add = cadd.Text;
            String type = urm_type.Text;
            #region validation
            bool valid = true;
            if (fnam.Trim() == "")
            {
                fname.BorderBrush = Brushes.Red;
                fname.BorderThickness = new Thickness(2, 2, 2, 2);
                valid = false;
                

            }
            if (!ccnic.IsMaskCompleted)
            {
                ccnic.BorderBrush = Brushes.Red;
                ccnic.BorderThickness = new Thickness(2, 2, 2, 2);
                valid = false;
                
            }

            if (type=="select_type")
            {
                

                urm_type.BorderBrush = Brushes.Red;
                urm_type.BorderThickness = new Thickness(2, 2, 2, 2);
                valid = false;
                

            }
            if (lnam.Trim() == "")
            {

                clnam.BorderBrush = Brushes.Red;
                clnam.BorderThickness = new Thickness(2, 2, 2, 2);
                valid = false;
                

            }
            if (add.Trim() == "")
            {

                cadd.BorderBrush = Brushes.Red;
                cadd.BorderThickness = new Thickness(2, 2, 2, 2);
                valid = false;
                // MessageBox.Show("Name should not be Empty", "Enter add name", MessageBoxButton.OK, MessageBoxImage.Stop);
                //return;

            }
            if (!ccont.IsMaskCompleted)
            {
                 ccont.BorderBrush = Brushes.Red;
                ccont.BorderThickness = new Thickness(2, 2, 2, 2);
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

            DateTime dbo = new DateTime();
            dbo = (DateTime)db.SelectedDate;
            DateTime ldo = new DateTime();
            ldo = (DateTime)ld.SelectedDate;
            

            cust.Address = add;
            cust.fname = fnam;
            cust.lastname = lnam;
            cust.cell_no = ph_no;

            cust.booking.booking_date = dbo;
            cust.booking.leave_date = ldo;



            cust.Address = add;
            cust.fname = fnam;
            cust.lastname = lnam;
            cust.cell_no = ph_no;

            hrs.update(cust, type);


            payment paym = new payment();
            TimeSpan diff = cust.booking.leave_date - cust.booking.booking_date;
            var days = diff.Days;
          
            if (type == "single bed")
            {
                int rnt = days * 5000;

                paym.rent = rnt;
                hrs.updatepaymnt(paym, cust);
            }
            else if (type == "double bed")
            {

                int rnt = days * 9000;
                paym.rent = rnt;
                hrs.updatepaymnt(paym, cust);
            }
            else 
            {

                System.Console.WriteLine("hhhhh");
            
            }

            

            clist = hrs.getcustomer();
            update_grid.ItemsSource = clist;
            update_grid.Items.Refresh();
            addgrid.ItemsSource = clist;
            addgrid.Items.Refresh();
            plist = hrs.getpaydata();
            bill_grid.ItemsSource = plist;
            bill_grid.Items.Refresh();

            fname.Text = null;
            clnam.Text = null;
            ccnic.Text = null;
            ccont.Text = null;

            cadd.Text = null;
            urm_type.Text = null;
        }



        private void Button_delete(object sender, RoutedEventArgs e) // customer
        {
            String fnam = fname.Text;
            String lnam = clnam.Text;
            String cuscnic = ccnic.Text;
            String ph_no = ccont.Text;
            String add = cadd.Text;
            #region validation
            bool valid = true;
            if (fnam.Trim() == "")
            {
                fname.BorderBrush = Brushes.Red;
                fname.BorderThickness = new Thickness(2, 2, 2, 2);
                valid = false;


            }
            if (!ccnic.IsMaskCompleted)
            {
                ccnic.BorderBrush = Brushes.Red;
                ccnic.BorderThickness = new Thickness(2, 2, 2, 2);
                valid = false;

            }

            
            if (lnam.Trim() == "")
            {

                clnam.BorderBrush = Brushes.Red;
                clnam.BorderThickness = new Thickness(2, 2, 2, 2);
                valid = false;


            }
            if (add.Trim() == "")
            {

                cadd.BorderBrush = Brushes.Red;
                cadd.BorderThickness = new Thickness(2, 2, 2, 2);
                valid = false;
                // MessageBox.Show("Name should not be Empty", "Enter add name", MessageBoxButton.OK, MessageBoxImage.Stop);
                //return;

            }
            if (!ccont.IsMaskCompleted)
            {
                ccont.BorderBrush = Brushes.Red;
                ccont.BorderThickness = new Thickness(2, 2, 2, 2);
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
            DateTime dbo = new DateTime();
            dbo = (DateTime)db.SelectedDate;
            DateTime ldo = new DateTime();
            ldo = (DateTime)ld.SelectedDate;
          
            cust.Address = add;
            cust.fname = fnam;
            cust.lastname = lnam;
            cust.cell_no = ph_no;
            cust.booking.booking_date = dbo;
            cust.booking.leave_date = ldo;
            hrs.deletebooking(cust);
            clist = hrs.getcustomer();
            update_grid.ItemsSource = clist;
            
            update_grid.Items.Refresh();
            addgrid.ItemsSource = clist;
            addgrid.Items.Refresh();
            plist = hrs.getpaydata();
            bill_grid.ItemsSource = plist;
            bill_grid.Items.Refresh();

            fname.Text = null;
            lnam = null;
            ccnic.Text = null;
            ccont.Text = null;
            cadd.Text = null;
        }

        private void searchrecord(object sender, RoutedEventArgs e) //search record
        {
            String items = searchtype.Text;
            record rcrd = new record();

           
            if (items == "name")
            {    
                String rnam = srnam.Text;
                if(rnam.Trim()=="")
                {
                    srnam.BorderBrush = Brushes.Red;
                    srnam.BorderThickness = new Thickness(2, 2, 2, 2);
                    MessageBox.Show("First_name required", "information", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
                rcrd.fname = rnam;
                hrs.getrecords(rcrd);
                srnam.Text = null;


            }
            else if (items == "cnic")
            {
                String rcni = srcni.Text;
                if (!srcni.IsMaskCompleted)
                {
                    srcni.BorderBrush = Brushes.Red;
                    srcni.BorderThickness = new Thickness(2, 2, 2, 2);
                    MessageBox.Show("Cnic# required", "information", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
                rcrd.CNIC_ = rcni;


                hrs.getrecords(rcrd);
                srcni.Text = null;


            }
            else if (items == "leaving date")
            {
                if (lvdate.Text == "")
                {
                    
                    MessageBox.Show("Leaving date required", "information", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
                DateTime lvda = new DateTime();
                lvda = (DateTime)lvdate.SelectedDate;

                rcrd.leaving_date = lvda;
                hrs.getrecords(rcrd);
                searchtype.IsEnabled = false;


            }
            else if (items == "booking date")
            {
                if (bkdate.Text == "")
                {
                    
                    MessageBox.Show("Booking date required", "information", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
                DateTime rbk = new DateTime();
                rbk = (DateTime)bkdate.SelectedDate;
                rcrd.Booking_date = rbk;

                hrs.getrecords(rcrd);
                searchtype.IsEnabled = false;


            }
            rlist = hrs.getrecords(rcrd);
            search_grid.ItemsSource = rlist;
            search_grid.Items.Refresh();



        }

        private void Searchcust(object sender, RoutedEventArgs e) // search record
        {
            String item = searchtype.Text;
            if (item == "cnic")
            {

                srcni.Visibility = Visibility.Visible;
                label.Content = "enter cnic no";
                search_btn.Visibility = Visibility.Visible;
                cancle_btn.Visibility = Visibility.Visible;
                ok.Visibility = Visibility.Hidden;
                searchtype.IsEnabled = false;

            }
            else if (item == "booking date")
            {

                bkdate.Visibility = Visibility.Visible;
                label.Content = "booking date";
                search_btn.Visibility = Visibility.Visible;
                cancle_btn.Visibility = Visibility.Visible;
                ok.Visibility = Visibility.Hidden;
                searchtype.IsEnabled = false;
            }
            else if (item == "leaving date")
            {

                lvdate.Visibility = Visibility.Visible;
                label.Content = "leaving date";
                search_btn.Visibility = Visibility.Visible;
                cancle_btn.Visibility = Visibility.Visible;
                ok.Visibility = Visibility.Hidden;
                searchtype.IsEnabled = false;
            }
            else if (item == "name")
            {

                srnam.Visibility = Visibility.Visible;
                label.Content = "enter name";
                search_btn.Visibility = Visibility.Visible;
                cancle_btn.Visibility = Visibility.Visible;
                ok.Visibility = Visibility.Hidden;
                searchtype.IsEnabled = false;
            }



        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            srcni.Visibility = Visibility.Hidden;
            srnam.Visibility = Visibility.Hidden;
            lvdate.Visibility = Visibility.Hidden;
            bkdate.Visibility = Visibility.Hidden;
            search_btn.Visibility = Visibility.Hidden;
            cancle_btn.Visibility = Visibility.Hidden;
            ok.Visibility = Visibility.Visible;
            label.Content = "";
            searchtype.IsEnabled = true;
            searchtype.Text = "selected type";
            rlist = hrs.getrecord();
            search_grid.ItemsSource = rlist;
            search_grid.Items.Refresh();

        }



        private void showbill(object sender, RoutedEventArgs e)
        {

           
            String name = bname.Text;
            String cnic = bcnic.Text;
            String rno = brno.Text;

            #region validation
            bool valid = true;
            if (name.Trim() == "")
            
            {
                bname.BorderBrush = Brushes.Red;
                bname.BorderThickness = new Thickness(2, 2, 2, 2);
                 valid = false;
            }

            if (!bcnic.IsMaskCompleted)
            {
                bcnic.BorderBrush = Brushes.Red;
                bcnic.BorderThickness = new Thickness(2, 2, 2, 2);
                valid = false;
            }
            if (rno.Trim() == "")
            {
                brno.BorderBrush = Brushes.Red;
                brno.BorderThickness = new Thickness(2, 2, 2, 2);
                valid = false;
                
            }
            if (!valid)
            {
                MessageBox.Show("First_name required", "information", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            
            }

            #endregion
            payment pay = new payment();
            pay.customer = new customer();
            pay.booking = new booking();
            pay.customer.fname = name;

            pay.customer.cnic = cnic;
            //MessageBox.Show(pay.customer.cnic);
            pay.booking.Room_ = int.Parse(rno);
            rnt.Visibility = Visibility.Visible;
            plist = hrs.getpaymnt(pay);
            bill_grid.ItemsSource = plist;
            bill_grid.Items.Refresh();
            rmsrv.Visibility = Visibility.Visible;
            /*bname.Text=null;
             bcnic.Text=null;
             brno.Text=null;*/
            ttl.Visibility = Visibility.Visible;

        }
        private void makebill(object sender, RoutedEventArgs e)
        {
            rnt.Visibility = Visibility.Hidden;

            String name = bname.Text;
            String cnic = bcnic.Text;
            String rno = brno.Text;
            #region validation
            bool valid = true;
            if (name.Trim() == "")
            {
                bname.BorderBrush = Brushes.Red;
                bname.BorderThickness = new Thickness(2, 2, 2, 2);
                valid = false;
            }

            if (!bcnic.IsMaskCompleted)
            {
                bcnic.BorderBrush = Brushes.Red;
                bcnic.BorderThickness = new Thickness(2, 2, 2, 2);
                valid = false;
            }
            if (rno.Trim() == "")
            {
                brno.BorderBrush = Brushes.Red;
                brno.BorderThickness = new Thickness(2, 2, 2, 2);
                valid = false;

            }
            if (!valid)
            {
                MessageBox.Show("First_name required", "information", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;

            }

            #endregion

            payment pay = new payment();
            pay.customer = new customer();
            pay.booking = new booking();
            
            pay.customer.fname = name;
            pay.customer.cnic = cnic;
            pay.booking.Room_ = int.Parse(rno);
            payment list = hrs.makebill(pay);
            //list = hrs.getpaymnt(pay);
            Switcher.Switch(new bill(list));

            ttl.Visibility = Visibility.Hidden;
            rmsrv.Visibility = Visibility.Hidden;
        }

        private void addgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void check_Click(object sender, RoutedEventArgs e)
        {

            String type = availble_combo.Text;
            room rm = new room();
            List<room> rmlist;
            if(type!="select room_type")
            {
            rm.room_type = type;
            rm.status = "unreserved";
            rmlist = hrs.checkavaiblty(rm);
            int availabel = rmlist.Count();

            rmavlbl.Visibility = Visibility.Visible;
            rmavlbl_text.Visibility = Visibility.Visible;
            rmavlbl_text.Text = availabel.ToString();
            next.IsEnabled = true;
        }
        else
         {
             MessageBox.Show("Select a type", "invalid Type", MessageBoxButton.OK, MessageBoxImage.Stop);
            
        }
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            availabl_rm.Visibility = Visibility.Hidden;
            bkings.Visibility = Visibility.Visible;
        }

        private void check_next(object sender, RoutedEventArgs e)
        {   
            #region validation
            bool valid = true;
            if (kit_rmno.Text== "")
            {
                kit_rmno.BorderBrush = Brushes.Red;
                kit_rmno.BorderThickness = new Thickness(2, 2, 2, 2);
                
                valid = false;

            }
            if (qunty.Text == "")
            {

                qunty.BorderBrush = Brushes.Red;
                qunty.BorderThickness = new Thickness(2, 2, 2, 2);
            
                valid = false;

            }

            if (!valid) 
            {

                MessageBox.Show("All fields are required", "information", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            #endregion
            String kitch_rm = kit_rmno.Text;
            String types = fdslec.Text;
            payment pay = new payment();
            pay.booking = new booking();
         
            pay.booking.Room_ = int.Parse(kitch_rm);
           

           

               if (types=="breakfast")
              {
                  String quant =qunty.Text;
                  int quanty = int.Parse(quant);
                  int amount = 700;
                  Console.WriteLine(quanty);
                 int total = quanty * amount;
                 Console.WriteLine(total);
                 hrs.rm_service(pay,total);
                 MessageBox.Show("1"+types);
              }
             else if (types=="lunch")
              {
                  String quant = qunty.Text;
                  int quanty = int.Parse(quant);
                  int amount = 1500;
                  int total = quanty * amount;
                  hrs.rm_service(pay, total);

              }
               else if (types=="dinner")
              {
                  String quant = qunty.Text;
                  int quanty = int.Parse(quant);
                  int amount = 1500;
                  int total = quanty * amount;
                  hrs.rm_service(pay, total);

              } 
               else if (types=="tea/coffee")
              {
                  String quant = qunty.Text;
                  int quanty = int.Parse(quant);
                  int amount = 200;
                  int total = quanty * amount;
                  hrs.rm_service(pay, total);

              }
               else if (types=="brunch")
              {
                  String quant = qunty.Text;
                  int quanty = int.Parse(quant);
                  int amount = 500;
                  int total = quanty * amount;
                  hrs.rm_service(pay, total);

              }
               else
               {
                   MessageBox.Show("invalid food selection", "information", MessageBoxButton.OK, MessageBoxImage.Stop);
                   return;
               }
              // kit_rmno.Text=null;
              // fdslec.Text=null;
          }
        #region validation
        private void fnam_TextChanged(object sender, TextChangedEventArgs e)
        {
            fnam.BorderBrush = Brushes.White;
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

        private void availble_combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void add_TextChanged(object sender, TextChangedEventArgs e)
        {
            add.BorderBrush = Brushes.White;
        } 
        #endregion

        private void fname_TextChanged(object sender, TextChangedEventArgs e)
        {
            fname.BorderBrush = Brushes.White;
        }

        private void clnam_TextChanged(object sender, TextChangedEventArgs e)
        {
            clnam.BorderBrush = Brushes.White;
        }

        private void ccnic_TextChanged(object sender, TextChangedEventArgs e)
        {
            ccnic.BorderBrush = Brushes.White;
        }

        private void ccont_TextChanged(object sender, TextChangedEventArgs e)
        {
            ccont.BorderBrush = Brushes.White;
        }

        private void cadd_TextChanged(object sender, TextChangedEventArgs e)
        {
           cadd.BorderBrush = Brushes.White;
        }

        private void srnam_TextChanged(object sender, TextChangedEventArgs e)
        {
            srnam.BorderBrush = Brushes.White;
        }

        private void srcni_TextChanged(object sender, TextChangedEventArgs e)
        {
            srcni.BorderBrush = Brushes.White;
        }

        private void kit_rmno_TextChanged(object sender, TextChangedEventArgs e)
        {
            kit_rmno.BorderBrush = Brushes.White;
        }

        private void qunty_TextChanged(object sender, TextChangedEventArgs e)
        {
            qunty.BorderBrush = Brushes.White;
        }

        private void bname_TextChanged(object sender, TextChangedEventArgs e)
        {
            bname.BorderBrush = Brushes.White;
        }

        private void bcnic_TextChanged(object sender, TextChangedEventArgs e)
        {
            bcnic.BorderBrush = Brushes.White;
        }

        private void brno_TextChanged(object sender, TextChangedEventArgs e)
        {
            brno.BorderBrush = Brushes.White;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MenuMain());
        }

    }
    }
