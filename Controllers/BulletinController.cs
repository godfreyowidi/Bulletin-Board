
using System;
using System.Collections.Generic;
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
    
    public IActionResult Index()
    {
      List<Bulletin> bulletinList = new List<Bulletin>();

      string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
      using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
      {
        connection.Open();

        using (NpgsqlCommand command = new NpgsqlCommand())
        {
          command.CommandText = "Select * From Bulletin";
          using(NpgsqlDataReader reader = command.ExecuteReader())
          {
            while(reader.Read())
            {
              Bulletin bulletin = new Bulletin();
              bulletin.BulletinID = Convert.ToInt32(reader["BulletinID"]);
              bulletin.Subject = Convert.ToString(reader["Subject"]);
              bulletin.Description = Convert.ToString(reader["Description"]);
              bulletin.TimeStamp = Convert.ToDateTime(reader["TimeStamp"]);
              bulletin.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
              //string subject = reader.GetString(reader.GetOrdinal("Subject"));
              //string description = reader.GetString(reader.GetOrdinal("Description"));

              bulletinList.Add(bulletin);
            }
          }
        }
        connection.Close();
      }
      return View(bulletinList);
    }
  }
}