using System.Diagnostics.Metrics;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
namespace DN_QT1_ASPWeb.Model
{
	public class Order
	{
		public string? OrderID { get; set; }
		public string? OrderDate { get; set; }
		public string? CustID { get; set; }

		public List<Order> GetOrderParameters(string connectionString)
		{
			List<Order> orderParameterList = new List<Order>();
			SqlConnection con = new SqlConnection(connectionString);
			string sqlQuery = "select OrderID, OrderDate, CustID from [Order]";
			con.Open();
			SqlCommand cmd = new SqlCommand(sqlQuery, con);
			SqlDataReader dr = cmd.ExecuteReader();
			if (dr != null)
			{
				while (dr.Read())
				{
					Order orderParameter = new Order();
					orderParameter.OrderID = dr["OrderID"].ToString();
					orderParameter.OrderDate = dr["OrderDate"].ToString();
					orderParameter.CustID = dr["CustID"].ToString();

					orderParameterList.Add(orderParameter);
				}
			}
			return orderParameterList;
		}
	}
}

