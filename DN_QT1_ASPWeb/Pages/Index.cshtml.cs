using DN_QT1_ASPWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DN_QT1_ASPWeb.Model;
namespace DN_QT1_ASPWeb.Pages
{
    public class IndexModel : PageModel
    {

		readonly IConfiguration _configuration;
		public List<Item> itemParameterList = new List<Item>();
		public string connectionString;
		public IndexModel(IConfiguration configuration)
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