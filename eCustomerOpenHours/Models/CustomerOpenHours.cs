namespace eCustomersAPI.Models
{
    public class CustomerOpenHours
    {
       public int CustomerId { get; set; }
       public int CustomerHoursId { get; set; }
       public string OpenHoursStart { get; set; }
       public string OpenHoursEnd { get; set; }
       public string Duration { get; set; }

    }
}
