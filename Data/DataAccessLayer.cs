using System;
using System.Collections.Generic;
using Npgsql;
using System.Data;

namespace HanmakTechnologies.BulletinBoard.Models
{
  public class DataAccessLayer
  {
    string connectionString = "Server=localhost;Port=5002;Database=bulletin_board;User Id=postgres;Password=L@x@d0nt@;";
    
    // To view all bulletin
    public IEnumerable<Bulletin> GetAllBulletin()
    {
      List<Bulletin> bulletinList = new List<Bulletin>();

      using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
      {
        NpgsqlCommand command = new NpgsqlCommand("spGetAllBulletin", connection);
        command.CommandType = CommandType.StoredProcedure;

        connection.Open();
        NpgsqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
          Bulletin bulletin = new Bulletin();

          bulletin.Id = Convert.ToInt32(reader["BulletinID"]);
          bulletin.Subject = reader["Subject"].ToString();
          bulletin.Description = reader["Description"].ToString();
          bulletin.TimePosted = Convert.ToDateTime(reader["TimeStamp"]);
          bulletin.DateCreated = Convert.ToDateTime(reader["DateCreated"]);

          bulletinList.Add(bulletin);
        }
        connection.Close();
      }
      return bulletinList;
    }
  

    // Create a bulletin
    public void AddBulletin(Bulletin bulletin)
    {
      using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
      {
        NpgsqlCommand command = new NpgsqlCommand("spAddBulletin", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@Subject", bulletin.Subject);
        command.Parameters.AddWithValue("@Description", bulletin.Description);

        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
      }
    }

    // Edit bulletin details
    public void EditBulletin(Bulletin bulletin)
    {
      using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
      {
        NpgsqlCommand command = new NpgsqlCommand("spEditBulletin", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@BulletinId", bulletin.Id);
        command.Parameters.AddWithValue("@Subject", bulletin.Subject);
        command.Parameters.AddWithValue("@Description", bulletin.Description);
        command.Parameters.AddWithValue("@TimeStamp", bulletin.TimePosted);
        command.Parameters.AddWithValue("@DateCreated", bulletin.DateCreated);

        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
      }
    }

    // Get details of a single bulletin
    public Bulletin GetBulletinData(int? id)
    {
      Bulletin bulletin = new Bulletin();

      using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
      {
        string sqlQuery = "SELECT * From tblBulletin WHERE BulletinId = " + id;
        NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection);

        connection.Open();
        NpgsqlDataReader reader = command.ExecuteReader();

        while(reader.Read())
        {
          bulletin.Id = Convert.ToInt32(reader["BullerinId"]);
          bulletin.Subject = reader["Subject"].ToString();
          bulletin.Description = reader["Description"].ToString();
          bulletin.TimePosted = Convert.ToDateTime(reader["TimeStamp"]);
          bulletin.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
        }
      }
      return bulletin;
    }

    // Delete record of a single bulletin
    public void DeleteBulletin(int? id)
    {
      using(NpgsqlConnection connection = new NpgsqlConnection(connectionString))
      {
        NpgsqlCommand command = new NpgsqlCommand("spDeleteBulletin", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@BulletinId", id);

        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
      }
    }
  }
}
