using System.ComponentModel;

namespace eCustomersWebApp.Models
{
    public class Customers
    {
        [DisplayName("Customer ID")]
        public int CustomerID { get; set; }
        
        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
