using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection.Metadata.Ecma335;
using System.Linq;

namespace eCustomersAPI.Models
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _eCustomers = "eCustomers";

        public CustomersRepository(IConfiguration configuration)
        {
            _configuration = configuration;            
        }

        public Customers GetCustomer(int customerId)
        {
            Customers customer = new Customers();          
            string sqlString = "select * from customers where customerid ='" + customerId + "';";

            DBContext DB = new DBContext(_configuration);
            DataTable customersDT = DB.ExecuteReaderDT(sqlString, _eCustomers);

            customer.CustomerID = Convert.ToInt32(customersDT.Rows[0][0]);
            customer.CustomerName = customersDT.Rows[0][1].ToString();
            customer.Address = customersDT.Rows[0][2].ToString();
            customer.Phone = customersDT.Rows[0][3].ToString();
            
            return customer;    
        }

        public List<Customers> GetCustomers()
        {
            List<Customers> customers = new List<Customers>();
            string sqlString = "select * from customers";          

            DBContext DB = new DBContext(_configuration);
            DataTable customersDT = DB.ExecuteReaderDT(sqlString, _eCustomers);

            foreach (DataRow row in customersDT.Rows)
            {
                customers.Add(new Customers()
                {
                    CustomerID = Convert.ToInt32(row[0]),
                    CustomerName = row[1].ToString(),
                    Address = row[2].ToString(),
                    Phone = row[3].ToString()
                });
            }

            return customers;
        }

        public int InsertCustomer(Customers customer)
        {
            int customerIdSequence = GetCustomerIDSequence();
            string insertSql = "insert into customers(customerId,name,address,phone) values ('"
                    + customerIdSequence + "','" + customer.CustomerName + "','" + customer.Address + "','" + customer.Phone + "');";
            
            DBContext DB = new DBContext(_configuration);
            Boolean status = DB.ExecuteCommand(insertSql, _eCustomers);

            return status ? customerIdSequence : 0; 
        }

        public int UpdateCustomer(Customers Customer)
        {
            string updateSql = "Update customers SET name = '" + Customer.CustomerName + "', address = '" + Customer.Address
                + "', phone = '" + Customer.Phone + "' where customerid = " + Customer.CustomerID.ToString() + ";";

            DBContext DB = new DBContext(_configuration);
            Boolean status = DB.ExecuteCommand(updateSql, _eCustomers);          
          
            return status ? Customer.CustomerID : 0;
        }

        public int DeleteCustomer(int customerID)
        {
            string deleteSql = "delete from customeropenhours where customerid = " + customerID.ToString() + ";";
            deleteSql = deleteSql + "delete from customers where customerid = " + customerID.ToString() + ";";

            DBContext DB = new DBContext(_configuration);
            Boolean status = DB.ExecuteCommand(deleteSql, _eCustomers);

            return status ? customerID : 0;
        }

        private int GetCustomerIDSequence()
        {
            try
            {
                string sqlString = "SELECT NEXT VALUE FOR Seq_CustomerID AS Counter";
                DBContext DB = new DBContext(_configuration);
                Int32 custID = Convert.ToInt32(DB.ExecuteScalar(sqlString, _eCustomers));
                              
                return custID;
            }
            catch (Exception)
            {
                throw;
            }    
        }
    }
}
