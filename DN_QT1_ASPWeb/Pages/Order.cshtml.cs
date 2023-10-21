using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DN_QT1_ASPWeb.Model;
namespace DN_QT1_ASPWeb.Pages
{
    public class OrderModel : PageModel
    {
		readonly IConfiguration _configuration;
		public List<Order> orderParameterList = new List<Order>();
		public string connectionString;
		public OrderModel(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public Order order = new Order();
		public void OnGet()
		{
			Order order = new Order();

			connectionString = _configuration.GetConnectionString("ConnectionString");

			orderParameterList = order.GetOrderParameters(connectionString);
		}
	}
}
