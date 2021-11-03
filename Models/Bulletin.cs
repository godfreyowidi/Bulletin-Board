
using System;

namespace HanmakTechnologies.BulletinBoard.Models
{
  public class Bulletin
  {
    public int Id { get; set; }
    public string Subject { get; set; }
    public string Description { get; set; }
    public DateTime TimePosted { get; set; }
    public DateTime DateCreated { get; set; }
    public int CategoryID { get; set; }


    public virtual Category Category { get; set; }
  }
}