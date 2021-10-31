
using System;

namespace HanmakTechnologies.BulletinBoard.Models
{
  public class Bulletin
  {
    public int BulletinID { get; set; }
    public string Subject { get; set; }
    public string Description { get; set; }
    public DateTime TimeStamp { get; set; }
    public DateTime DateCreated { get; set; }
    public int CategoryID { get; set; }


    public virtual Category Category { get; set; }
  }
}