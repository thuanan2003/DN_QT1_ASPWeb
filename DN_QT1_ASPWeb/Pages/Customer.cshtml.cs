using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using DN_QT1_ASPWeb.Model;
using System.Diagnostics.Metrics;

namespace DN_QT1_ASPWeb.Pages
{
	public class CustomerModel : PageModel
	{
		readonly IConfiguration _configuration;
		public List<Customer> custParameterList = new List<Customer>();
		public string connectionString;
		public Customer cust = new Customer();
		public CustomerModel(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public void OnGet()
		{
			Customer cust = new Customer();
			connectionString = _configuration.GetConnectionString("ConnectionString");
			custParameterList = cust.GetCustParameters(connectionString);
		}
	}
}

