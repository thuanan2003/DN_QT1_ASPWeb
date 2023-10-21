using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using DN_QT1_ASPWeb.Model;
using System.Diagnostics.Metrics;
using DN_QT1_ASPWeb.Data;
using Microsoft.Data.SqlClient;

namespace DN_QT1_ASPWeb.Pages
{
    public class ItemModel : PageModel
    {
		
		readonly IConfiguration _configuration;
		public List<Item> itemParameterList = new List<Item>();
		public string connectionString;
		public ItemModel(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		
		public Item item = new Item();
		public void OnGet()
		{
			Item item = new Item();

			connectionString = _configuration.GetConnectionString("ConnectionString");

			itemParameterList = item.GetItemParameters(connectionString);
		}
		
	}
}
