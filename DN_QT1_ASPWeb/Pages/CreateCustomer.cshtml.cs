using DN_QT1_ASPWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DN_QT1_ASPWeb.Pages
{
	[BindProperties]
	public class CreateCustomerModel : PageModel
    {
		public string errorMess = "";
		public string successMess = "";
		readonly IConfiguration _configuration;
		public List<Customer> custParameterList = new List<Customer>();
		public string connectionString;
		public CreateCustomerModel(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public Customer Customer { get; set; }
		public Customer customer = new Customer();
		
		public void OnGet()
		{
			Customer cust = new Customer();

			connectionString = _configuration.GetConnectionString("ConnectionString");

			custParameterList = cust.GetCustParameters(connectionString);
		}
		public void OnPost(string custid, string custname, string address)
		{
			Customer getcust = new Customer();
			connectionString = _configuration.GetConnectionString("ConnectionString");
			custParameterList = getcust.GetCustParameters(connectionString);


			string conn = "Data Source=THUAN-AN\\SQLEXPRESS;Initial Catalog=DN_QT1;Integrated Security=True;TrustServerCertificate=true";
			customer.CustID = Request.Form["CustID"];
			customer.CustName = Request.Form["CustName"];
			customer.Address = Request.Form["Address"];
			
			if (customer.CustID.Length == 0 || customer.CustName.Length == 0 || customer.Address.Length == 0)
			{

				errorMess = "Please fill all the form";
				return;

			}

			try
			{
				using (SqlConnection connection = new SqlConnection(conn))
				{
					connection.Open();
					string sql = "INSERT INTO Customer " +
						"(CustID,CustName, Address) VALUES" +
						"(@CustID,@CustName,@Address);";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("CustID", customer.CustID);
						command.Parameters.AddWithValue("CustName", customer.CustName);
						command.Parameters.AddWithValue("Address", customer.Address);
						
						command.ExecuteNonQuery();
					}
				}

			}
			catch (Exception ex)
			{
				errorMess = ex.Message;
				return;
			}
			
			customer.CustName = "";
			customer.Address = "";
			successMess = "Added Customer";

			Response.Redirect("/Customer");
		}
	}
}
