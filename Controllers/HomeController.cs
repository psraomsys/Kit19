using Kit19;
using Kit19.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

public class HomeController : Controller
{
    private readonly ProductDbContext _context; // Your database context

    public HomeController(ProductDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(ProductSearchViewModel model)
    {
        if (model == null)
        {
            model = new ProductSearchViewModel();
        }

        if (model.Conjunction != "AND" && model.Conjunction != "OR")
        {
            model.Conjunction = "AND";
        }

        model.SearchResults = SearchProducts(model);

        return View(model);
    }

    private List<Product> SearchProducts(ProductSearchViewModel model)
    {
        var connection = _context.Database.GetDbConnection();
        var results = new List<Product>();

        try
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SearchProducts";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@ProductName", model.ProductName ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Size", model.Size ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Price", model.Price ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@MfgDate", model.MfgDate ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Category", model.Category ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Conjunction", model.Conjunction));

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var product = new Product
                        {
                            ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                            ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                            Size = reader.GetString(reader.GetOrdinal("Size")),
                            Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                            MfgDate = reader.GetDateTime(reader.GetOrdinal("MfgDate")),
                            Category = reader.GetString(reader.GetOrdinal("Category"))
                        };
                        results.Add(product);
                    }
                }
            }
        }
        finally
        {
            connection.Close();
        }

        return results;
    }
}
