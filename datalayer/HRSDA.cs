using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datalayer
{
    class HRSDA
    {
        hrsEntitiess hrs;
        public HRSDA()
        {
            hrs = new hrsEntitiess();
        }
        public admin getadmin(String admin, String pass)
        {
            return hrs.admins.Where(y => (y.NAME == admin && y.password == pass)).FirstOrDefault();
        }
        public user getusr(String usr, String usr_pass)
        {
            return hrs.users.Where(y => (y.user_name == usr && y.password == usr_pass)).FirstOrDefault();
        }
        public void adduser(user u)
        {
            hrs.users.Add(u);
            hrs.SaveChanges();

        }
        public List<customer> getcustomer()
        {

            return hrs.customers.ToList();


        }
        public List<booking> getbooking()
        {

            return hrs.bookings.ToList();
        }

        public List<record> getrecord()
        {
            return hrs.records.ToList();
        }
        public List<payment> getpay()
        {
            return hrs.payments.ToList();
        }

        public void addcust(customer cus)
        {
           
            hrs.customers.Add(cus);
            hrs.SaveChanges();


        }
        public void addbooking(booking bk, customer cus, String rm_type)

        {
            
            customer cust = hrs.customers.Where(x => (x.fname == cus.fname && x.lastname == cus.lastname && x.cell_no == cus.cell_no && x.cnic == cus.cnic)).FirstOrDefault();
            bk.cust_id = cust.cust_id;
            room roo = hrs.rooms.Where(x => (x.room_type == rm_type && x.status == "unreserved")).FirstOrDefault();


            bk.Room_ = roo.room_;
            hrs.bookings.Add(bk);
            hrs.SaveChanges();
            roo.status = "reserved";
            hrs.SaveChanges();
            booking bok = hrs.bookings.Where(x => (x.cust_id == cust.cust_id)).FirstOrDefault();
            int cbok = bok.Booking_id;
            cust.booking_id = cbok;
            hrs.SaveChanges();
        }
       public void addpayment(customer cus, booking bk,payment pay)
        {
            customer cust = hrs.customers.Where(x => (x.fname == cus.fname && x.lastname == cus.lastname && x.cell_no == cus.cell_no && x.cnic == cus.cnic)).FirstOrDefault();
            pay.cust_id = cust.cust_id;
            pay.Booking_id = cust.booking.Booking_id;
            hrs.payments.Add(pay);
            hrs.SaveChanges();

        }
        public void addroom(room rm)
        {
            hrs.rooms.Add(rm);
            hrs.SaveChanges();
        }

        public customer getcnic(String cnic_no)
        {

            return hrs.customers.Where(x => (x.cnic == cnic_no)).FirstOrDefault();

        }

        public void update(customer cust,String type)
        {
           
          
            customer custme = hrs.customers.Where(x => (x.cust_id == cust.cust_id)).FirstOrDefault();
            booking bkg = hrs.bookings.Where(x => (x.Booking_id == cust.booking.Booking_id)).FirstOrDefault();
            ;
            if (type==custme.booking.room.room_type)
            {
                hrs.Entry(custme).CurrentValues.SetValues(cust);
                hrs.Entry(bkg).CurrentValues.SetValues(cust.booking);
                hrs.SaveChanges();
            }
            else
            {
               
                room roo = hrs.rooms.Where(y => (y.room_ == cust.booking.Room_)).FirstOrDefault(); // for changing room ststus
           
                room rm = hrs.rooms.Where(y => (y.room_type == type && y.status == "unreserved")).FirstOrDefault();
                cust.booking.Room_ = rm.room_;
                rm.status = "reserved";
                cust.booking.room.status = rm.status;
                hrs.Entry(custme).CurrentValues.SetValues(cust);
               
                hrs.Entry(bkg).CurrentValues.SetValues(cust.booking);
                
                roo.status = "unreserved";
                hrs.SaveChanges();
            }
            
            



        }
        public void updatepaymnt(payment paym,customer cust) 
        {

            payment pay=hrs.payments.Where(x => (x.cust_id == cust.cust_id)).FirstOrDefault();
            pay.rent = paym.rent;
            hrs.SaveChanges();

        }

        public void deletebooking(customer cust)

        {
            customer custme = hrs.customers.Where(x => (x.cust_id == cust.cust_id)).FirstOrDefault();
            booking bok = hrs.bookings.Where(x => (x.Booking_id == cust.booking_id)).FirstOrDefault();
           System.Console.WriteLine(bok.booking_date);
            System.Console.WriteLine(bok.Booking_id);
            System.Console.WriteLine(bok.cust_id);
            System.Console.WriteLine(bok.Room_);
            System.Console.WriteLine(bok.leave_date);
            payment py = hrs.payments.Where(x => (x.cust_id == cust.cust_id)).FirstOrDefault();
            hrs.customers.Remove(custme);
            hrs.SaveChanges();
            hrs.bookings.Remove(bok);
            hrs.payments.Remove(py);
            hrs.SaveChanges();
            }

        public void deleteuser(user dur)
        {
           user usr= hrs.users.Where(x => (x.user_name == dur.user_name && x.cnic_ == dur.cnic_)).FirstOrDefault();

            hrs.users.Remove(usr);
            hrs.SaveChanges();

        }
        public List<record> getrecords(record rcrd)
        {

            return hrs.records.Where(x => (x.fname == rcrd.fname || x.CNIC_ == rcrd.CNIC_ || x.leaving_date == rcrd.leaving_date || x.Booking_date == rcrd.Booking_date)).ToList();

        }
        public List<payment> getpaymnt(payment pay) // for bill generation
        {

           payment pmnt= hrs.payments.Where(x => (x.customer.cnic == pay.customer.cnic)).FirstOrDefault();
           pay.total= pmnt.rent + pmnt.room_service;
           pmnt.total = pay.total;
           hrs.SaveChanges();
            return hrs.payments.Where(x => (x.customer.fname == pay.customer.fname && x.customer.cnic==pay.customer.cnic && x.booking.Room_==pay.booking.Room_)).ToList();
        
        }
        public payment makebill(payment pay) // for bill generation
        {

         
                payment payd=hrs.payments.Where(x => ( x.customer.cnic==pay.customer.cnic)).FirstOrDefault();
                pay.total = payd.rent + payd.room_service;
                payd.total = pay.total;
                hrs.SaveChanges();
                return hrs.payments.Where(x => (x.customer.fname == pay.customer.fname && x.customer.cnic == pay.customer.cnic && x.booking.Room_ == pay.booking.Room_)).FirstOrDefault();

        }
        public customer getpaymnt_data(customer cutm) 
        {
            return hrs.customers.Where(x => (x.fname == cutm.fname && x.cnic == cutm.cnic && x.booking.Room_ == cutm.booking.Room_)).FirstOrDefault();
        }

        public payment cust_paid(customer custe)
        {
            customer cutmr = hrs.customers.Where(x => (x.cnic == custe.cnic)).FirstOrDefault();

            return hrs.payments.Where(x => (x.cust_id == cutmr.cust_id)).FirstOrDefault();
        }
        public void paid(record rcd) 
        {

            hrs.records.Add(rcd);
            
            hrs.SaveChanges();
            customer custm=hrs.customers.Where(x=>(x.cnic==rcd.CNIC_)).FirstOrDefault();
            payment pay = hrs.payments.Where(x => (x.cust_id == custm.cust_id)).FirstOrDefault();
            booking bk = hrs.bookings.Where(x => (x.Booking_id == pay.Booking_id)).FirstOrDefault();
            bk.room.status = "unreserved";
            hrs.SaveChanges();
            hrs.bookings.Remove(bk);
            hrs.payments.Remove(pay);
            hrs.customers.Remove(custm);
            hrs.SaveChanges();
        }
        public List<room> checkavaiblty(room rm)
        {
            return hrs.rooms.Where(x => (x.room_type == rm.room_type && x.status==rm.status)).ToList();
        
        }
        public void rm_service(payment pay,int total)
        {
            payment pays = hrs.payments.Where(x => (x.booking.Room_ == pay.booking.Room_)).FirstOrDefault();

            if (pays.room_service == null)
            {

                pay.room_service = total;
                pays.room_service = pay.room_service;
            }
            else
            {
                int? ppp = pays.room_service;
                int amnt = total + ppp ?? default(int);
                Console.WriteLine(amnt);
                pay.room_service = amnt;
                Console.WriteLine(pay.room_service);
                pays.room_service = pay.room_service;
            }
            // hrs.Entry(pays).CurrentValues.SetValues(pay.room_service);
            hrs.SaveChanges();
        }

        public List<payment> getpaydata()
        {

            return hrs.payments.ToList();
        }
    }
    }
