using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using HanmakTechnologies.BulletinBoard.Models;

namespace HanmakTechnologies.BulletinBoard
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
    
    public IActionResult Edit(int id)
    {
      string connectionString = Configuration["ConnectionStrings:DefaultConnection"];

      Bulletin bulletin = new Bulletin();
      using(NpgsqlConnection connection = new NpgsqlConnection(connectionString))
      {
        connection.Open();
        using (NpgsqlCommand command = new NpgsqlCommand())
        {
          command.CommandText = "Select * From Bulletin Where BulletID='{id}'";

          using(NpgsqlDataReader reader = command.ExecuteReader())
          {
            while(reader.Read())
            {
              //Bulletin bulletin = new Bulletin();
              bulletin.BulletinID = Convert.ToInt32(reader["BulletinID"]);
              bulletin.Subject = Convert.ToString(reader["Subject"]);
              bulletin.Description = Convert.ToString(reader["Description"]);
              bulletin.TimeStamp = Convert.ToDateTime(reader["TimeStamp"]);
              bulletin.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
            }
          }
          connection.Close();
        }
        return View(bulletin);
      }  
    }

    [HttpPost]
    public IActionResult Edit(Bulletin bulletin, int id)
    {
      string connectionString = Configuration["ConnectionStrings:DefaultConnection"];

      using(NpgsqlConnection connection = new NpgsqlConnection(connectionString))
      {
        connection.Open();

        using (NpgsqlCommand command = new NpgsqlCommand())
        {
          command.CommandText = "UPDATE Bulletin SET Subject='{bulletin.Subject}', Description='{bulletin.Description}' Where Id='{id}'";

          command.ExecuteNonQuery();
          connection.Close();
        }
      }
      return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
      string connectionString = Configuration["ConnectionStrings:DefaultConnection"];

      using(NpgsqlConnection connection = new NpgsqlConnection(connectionString))
      {
        connection.Open();

        using (NpgsqlCommand command = new NpgsqlCommand())
        {
          command.CommandText = "Delete From Bulletin Where BulletinID='{id}'";

          command.ExecuteNonQuery();
          connection.Close();
        }
      }
      return RedirectToAction("Index");
    }
  }
}
