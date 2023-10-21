using System.Diagnostics.Metrics;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DN_QT1_ASPWeb.Model
{
	public class Item
	{
		public string? ItemID { get; set; }
		public string? ItemName { get; set; }
		public string? Size { get; set; }
		public float Price { get; set; }
		
		public List<Item> GetItemParameters(string connectionString)
		{
			List<Item> itemParameterList = new List<Item>();
			SqlConnection con = new SqlConnection(connectionString);
			string sqlQuery = "select ItemID, ItemName, Size, Price from Item";
			con.Open();
			SqlCommand cmd = new SqlCommand(sqlQuery, con);
			SqlDataReader dr = cmd.ExecuteReader();
			if (dr != null)
			{
				while (dr.Read())
				{
					Item itemParameter = new Item();
					itemParameter.ItemID = dr["ItemID"].ToString();
					itemParameter.ItemName = dr["ItemName"].ToString();
					itemParameter.Size = dr["Size"].ToString();
					itemParameter.Price = float.Parse(dr["Price"].ToString());
					itemParameterList.Add(itemParameter);
				}
			}
			return itemParameterList;
		}
		
		
		
	}
}
