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

using datalayer;
using System.Windows.Shapes;


namespace HOTEL_RESERVATION_SYSTEM
{
    /// <summary>
    /// Interaction logic for bill.xaml
    /// </summary>
    public partial class bill : UserControl
    {
        customer custe=new customer();
        String cni;
        HRSFACTORY hrs;
        
        public bill()
        {

            InitializeComponent();
            hrs = new HRSFACTORY();
            stmp.Visibility = Visibility.Hidden;
        }
        public bill(  payment list):this()
        {
            InitializeComponent();
           

            cni = list.customer.cnic;
            nam.Content = list.customer.fname;
            fnam.Content = list.customer.lastname;
            cnic.Content = list.customer.cnic;
            cell.Content = list.customer.cell_no;
            add.Content = list.customer.Address;
            rmno.Content = list.booking.Room_;
            bk.Content = list.booking.booking_date;
            lv.Content = list.booking.leave_date;
            rent.Content = list.rent;
            amount.Content = list.total;
            service.Content = list.room_service;
            amount.Content = list.total;
        }

        private void paid(object sender, RoutedEventArgs e)
        {
        
            custe.cnic = cni;
            payment pay= hrs.cust_paid(custe);
            record rcd = new record();
            rcd.Address = pay.customer.Address;
            rcd.amount = pay.rent;
            rcd.Booking_date = pay.booking.booking_date;
            rcd.cell_no = pay.customer.cell_no;
            rcd.CNIC_ = pay.customer.cnic;
            rcd.fname = pay.customer.fname;
            rcd.lastname = pay.customer.lastname;
            rcd.leaving_date = pay.booking.leave_date;
            rcd.room_ = pay.booking.Room_;
            hrs.paid(rcd);
            
          
            stmp.Visibility = Visibility.Visible;
            //MessageBox.Show(cni);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new UserMaintainer());
        }
      
    }
}
