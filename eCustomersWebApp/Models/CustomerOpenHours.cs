using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eCustomersWebApp.Models
{
    public class CustomerOpenHours
    {
        [DisplayName("Customer ID")]
        public int CustomerId { get; set; }

        [DisplayName("Open Hours ID")]
        public int CustomerHoursId { get; set; }

        [DisplayName("Open Hours Start")]
        public DateTime OpenHoursStart { get; set; }

        [DisplayName("Open Hours End")]
        public DateTime OpenHoursEnd { get; set; }
    }
}
