using System.Diagnostics.Metrics;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
namespace DN_QT1_ASPWeb.Model
{
	public class Customer
	{
		public string? CustID { get; set; }
		public string? CustName { get; set; }
		public string? Address { get; set; }
		
		public List<Customer> GetCustParameters(string connectionString)
		{
			List<Customer> custParameterList = new List<Customer>();
			SqlConnection con = new SqlConnection(connectionString);
			string sqlQuery = "select CustID, CustName, Address from Customer";
			con.Open();
			SqlCommand cmd = new SqlCommand(sqlQuery, con);
			SqlDataReader dr = cmd.ExecuteReader();
			if (dr != null)
			{
				while (dr.Read())
				{
					Customer custParameter = new Customer();
					custParameter.CustID = dr["CustID"].ToString();
					custParameter.CustName = dr["CustName"].ToString();
					custParameter.Address = dr["Address"].ToString();
					
					custParameterList.Add(custParameter);
				}
			}
			return custParameterList;
		}
	}
}
