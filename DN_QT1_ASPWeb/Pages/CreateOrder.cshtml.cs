using DN_QT1_ASPWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DN_QT1_ASPWeb.Pages
{
	[BindProperties]
	public class CreateOrderModel : PageModel
	{
		public string errorMess = "";
		public string successMess = "";
		readonly IConfiguration _configuration;
		public List<Order> orderParameterList = new List<Order>();
		public string connectionString;
		public CreateOrderModel(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public Order Order { get; set; }
		public Order order = new Order();

		public void OnGet()
		{
			Order cust = new Order();

			connectionString = _configuration.GetConnectionString("ConnectionString");

			orderParameterList = order.GetOrderParameters(connectionString);
		}
		public void OnPost(string orderid, string ordername, string address)
		{
			Order getorder = new Order();
			connectionString = _configuration.GetConnectionString("ConnectionString");
			orderParameterList = getorder.GetOrderParameters(connectionString);


			string conn = "Data Source=THUAN-AN\\SQLEXPRESS;Initial Catalog=DN_QT1;Integrated Security=True;TrustServerCertificate=true";
			order.OrderID = Request.Form["OrderID"];
			order.OrderDate = Request.Form["OrderDate"];
			order.CustID = Request.Form["CustID"];

			if (order.OrderID.Length == 0 || order.OrderDate.Length == 0 || order.CustID.Length == 0)
			{

				errorMess = "Please fill all the form";
				return;

			}

			try
			{
				using (SqlConnection connection = new SqlConnection(conn))
				{
					connection.Open();
					string sql = "INSERT INTO [Order] " +
						"(OrderID,OrderDate, CustID) VALUES" +
						"(@OrderID,@OrderDate,@CustID);";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("OrderID", order.OrderID);
						command.Parameters.AddWithValue("OrderDate", order.OrderDate);
						command.Parameters.AddWithValue("CustID", order.CustID);

						command.ExecuteNonQuery();
					}
				}

			}
			catch (Exception ex)
			{
				errorMess = ex.Message;
				return;
			}

			order.OrderDate = "";
			order.CustID = "";
			successMess = "Added Order";

			Response.Redirect("/Order");
		}
	}
}
