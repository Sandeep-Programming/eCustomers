using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace eCustomersAPI.Models
{
    public class CustomerOpenHoursRepository : ICustomerOpenHoursRepository
    {

        private readonly IConfiguration _configuration;
        private readonly string _eCustomers = "eCustomers";
        public CustomerOpenHoursRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<CustomerOpenHours> GetCustomerOpenHours(int customerId)
        {
            string sqlString = "select * from customeropenhours where customerid = '" + customerId + "';";

            List<CustomerOpenHours> customerOpenHours = new List<CustomerOpenHours>();

            DBContext DB = new DBContext(_configuration);
            DataTable customerOpenHoursDT = DB.ExecuteReaderDT(sqlString, _eCustomers);

            foreach (DataRow row in customerOpenHoursDT.Rows)
            {
                customerOpenHours.Add(new CustomerOpenHours()
                {
                    CustomerId = Convert.ToInt32(row[0]),
                    CustomerHoursId = Convert.ToInt32(row[1]),
                    OpenHoursStart = row[2].ToString(),
                    OpenHoursEnd = row[3].ToString()
                });
            }

            return customerOpenHours;
        }

        public CustomerOpenHours GetOneCustomerOpenHour(int customerId, int customerHourId)
        {
            string sqlString = "select * from customeropenhours where customerid = '" + customerId + "' and customerhoursid = '" + customerHourId + "';";
            CustomerOpenHours customerOpenHour = new CustomerOpenHours();

            DBContext DB = new DBContext(_configuration);
            DataTable customerOpenHourDT = DB.ExecuteReaderDT(sqlString, _eCustomers);

            customerOpenHour.CustomerId = Convert.ToInt32(customerOpenHourDT.Rows[0][0]);
            customerOpenHour.CustomerHoursId = Convert.ToInt32(customerOpenHourDT.Rows[0][1]);
            customerOpenHour.OpenHoursStart = customerOpenHourDT.Rows[0][2].ToString();
            customerOpenHour.OpenHoursEnd = customerOpenHourDT.Rows[0][3].ToString();

            return customerOpenHour;
        }
        public int InsertCustomerOpenHours(CustomerOpenHours customerOpenHours)
        {
            int customerHourIdSequence = GetCustomerHourIDSequence();
            string insertSql = "insert into customeropenhours (customerid, customerhoursid, openhoursstart, openhoursend) values ('"
                    + customerOpenHours.CustomerId + "','" + customerHourIdSequence + "','"
                    + customerOpenHours.OpenHoursStart + "','" + customerOpenHours.OpenHoursEnd + "');";

            DBContext DB = new DBContext(_configuration);
            Boolean status = DB.ExecuteCommand(insertSql, _eCustomers);

            return status ? customerHourIdSequence : 0;
        }

        public int UpdateCustomerOpenHours(CustomerOpenHours customerOpenHours)
        {
            string updateSql = "Update customeropenhours SET openhoursstart = '" + customerOpenHours.OpenHoursStart + "', openhoursend = '" + customerOpenHours.OpenHoursEnd
             + "'  where customerhoursid = " + customerOpenHours.CustomerHoursId.ToString() + ";";

            DBContext DB = new DBContext(_configuration);
            Boolean status = DB.ExecuteCommand(updateSql, _eCustomers);

            return status ? customerOpenHours.CustomerHoursId : 0;
        }
        public int DeleteCustomerOpenHours(int customeId, int customerHoursId)
        {
            string deleteSql = "delete from customeropenhours where customerid = '" + customeId + "' and customerhoursid = '" + customerHoursId + "';";

            DBContext DB = new DBContext(_configuration);
            Boolean status = DB.ExecuteCommand(deleteSql, _eCustomers);

            return status ? customerHoursId : 0;
        }

        private int GetCustomerHourIDSequence()
        {
            try
            {
                string sqlString = "SELECT NEXT VALUE FOR Seq_CustomerHoursID AS Counter";

                DBContext DB = new DBContext(_configuration);
                Int32 custHourId = Convert.ToInt32(DB.ExecuteScalar(sqlString, _eCustomers));

                return custHourId;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
