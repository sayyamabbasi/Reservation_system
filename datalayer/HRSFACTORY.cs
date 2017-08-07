using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace datalayer
{
    public class HRSFACTORY
    {
        HRSDA hrsda;
        public HRSFACTORY()
        {
            hrsda = new HRSDA();
        }
        public admin getadmin(String admin, String pass)
        {
            return hrsda.getadmin(admin, pass);
        }
        public user getusr(String usr, String usr_pass)
        {
            return hrsda.getusr(usr, usr_pass);
        }
        public void adduser(user u)
        {
            hrsda.adduser(u);

        }
        public List<customer> getcustomer()
        {
            return hrsda.getcustomer();
        }
        public List<payment> getpaymnt(payment pay)
        {

            return hrsda.getpaymnt(pay);


        }
        public List<booking> getbooking()
        {

            return hrsda.getbooking();
        }
        public List<record> getrecord()
        {
            return hrsda.getrecord();
        }

        public void addcust(customer cus)
        {
            hrsda.addcust(cus);

        }
        public void addbooking(booking bk,customer cus,String type)
        {
            hrsda.addbooking(bk,cus,type);


        }
        public customer getcnic(String cnic_no)
        {
            return hrsda.getcnic(cnic_no);
        
        }

        public void addroom(room rm ,int rm_no)
    {
        for (int i = 0; i < rm_no; i++)
        {
            hrsda.addroom(rm);
        }
    
    }
        public void update(customer cust,String type)
        {
            hrsda.update(cust,type);
        
        }
        
        public List<payment> getpay()
        {
            return hrsda.getpay();
        }

        public void deletebooking(customer cust)
        {

            hrsda.deletebooking(cust);
        
        }
        public void deleteuser(user dur) 
        {
            hrsda.deleteuser(dur);
        }

        public List<record> getrecords(record rcrd)
        {

            return hrsda.getrecords(rcrd);
        
        
        }

        public void updatepaymnt(payment paym, customer cust) 
        {

            hrsda.updatepaymnt(paym, cust);
        
        }
        
        public payment makebill(payment pay)
        {
            return hrsda.makebill(pay);
        }
        public void addpayment(customer cus, booking bk, payment pay)
        {
            hrsda.addpayment(cus, bk, pay);
        }
        
        public customer getpaymnt_data(customer cutm)
        {
            return hrsda.getpaymnt_data(cutm);
        }

        public List<payment> getpaydata()
        {
            return hrsda.getpaydata();
        
        }
        public payment cust_paid (customer custe)
        {
           return hrsda.cust_paid(custe);
    
        }
        public void paid(record rcd)
        {
            hrsda.paid(rcd);
        }
        public List<room> checkavaiblty(room rm)
        {
            return hrsda.checkavaiblty(rm);
        
        }

        public void rm_service(payment pay, int total)
        {
            hrsda.rm_service(pay,total);
        
        }
    }
}
