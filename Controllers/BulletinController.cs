
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace HalmakTechnologies.BulletinBoard
{
  public class BulletinController : Controller
  {
    public IConfiguration Configuration { get; }
    public BulletinController(IConfiguration configuration) 
    {
      Configuration = configuration;
    }

    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Create(Bulletin bulletin)
    {
      
      string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
      using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
      {

        connection.Open();
        
        using (NpgsqlCommand command = new NpgsqlCommand())
        {
          command.CommandText = "insert into bulletin (Subject, Desccription) VALUES (@subject, @description)";
          command.Parameters.AddWithValue("@subject");
          command.Parameters.AddWithValue("Description");

          var newID = (int)command.ExecuteScalar();
        }
      }
      return View();
    }
  }
}