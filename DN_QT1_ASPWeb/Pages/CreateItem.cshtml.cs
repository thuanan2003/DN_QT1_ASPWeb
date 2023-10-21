using DN_QT1_ASPWeb.Data;
using DN_QT1_ASPWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Drawing;

namespace DN_QT1_ASPWeb.Pages
{
    [BindProperties]
    public class CreateItemModel : PageModel
    {
		public string errorMess = "";
		public string successMess = "";
		readonly IConfiguration _configuration;
		public List<Item> itemParameterList = new List<Item>();
		public string connectionString;
		public CreateItemModel(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public Item Item { get; set; }
		public Item item = new Item();
		/*private readonly AppDbContext _db;
		public CreateItemModel(AppDbContext db)
        {
            _db = db;
        }
		public async Task<ActionResult > OnPost(Item item)
        {
            await _db.Items.AddAsync(item);
            await _db.AddRangeAsync();
            return RedirectToPage("Item");
        }
		*/
		public void OnGet()
        {
			Item item = new Item();
			
			connectionString = _configuration.GetConnectionString("ConnectionString");
			
			itemParameterList = item.GetItemParameters(connectionString);
		}
		public void OnPost(string itemid, string itemname, string size, float price)
		{
			Item getitem = new Item();
			connectionString = _configuration.GetConnectionString("ConnectionString");
			itemParameterList = getitem.GetItemParameters(connectionString);


			string conn = "Data Source=THUAN-AN\\SQLEXPRESS;Initial Catalog=DN_QT1;Integrated Security=True;TrustServerCertificate=true";
			item.ItemID = Request.Form["ItemID"];
			item.ItemName = Request.Form["ItemName"];
			item.Size = Request.Form["Size"];
			item.Price = float.Parse(Request.Form["Price"]);
			if (item.ItemID.Length == 0 || item.ItemName.Length == 0 || item.Size.Length == 0)
			{

				errorMess = "Please fill all the form";
				return;

			}

			try
			{
				using (SqlConnection connection = new SqlConnection(conn))
				{
					connection.Open();
					string sql = "INSERT INTO Item " +
						"(ItemID,ItemName, Size,Price) VALUES" +
						"(@ItemID,@ItemName,@Size,@Price);";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("ItemID", item.ItemID);
						command.Parameters.AddWithValue("ItemName", item.ItemName);
						command.Parameters.AddWithValue("Size", item.Size);
						command.Parameters.AddWithValue("Price", item.Price);
						command.ExecuteNonQuery();
					}
				}

			}
			catch (Exception ex)
			{
				errorMess = ex.Message;
				return;
			}
			
			item.ItemName = "";
			item.Size = "";
			item.Price = 0;
			successMess = "Added Item";

			Response.Redirect("/Item");
		}
	}

		/*readonly IConfiguration _configuration;
		public List<Item> itemParameterList = new List<Item>();
		public string connectionString;
		public CreateItemModel(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public void OnPost(string itemid, string itemname, string size, float price)
		{
			Item item = new Item();
            item.ItemID = itemid;
            item.ItemName = itemname;
            item.Size = size;
            item.Price = price;
			connectionString = _configuration.GetConnectionString("ConnectionString");
            item.InsertItem(item,connectionString);
			itemParameterList = item.GetItemParameters(connectionString);
		}*/
	
}
