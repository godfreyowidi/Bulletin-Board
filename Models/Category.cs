
using System.Collections.Generic;

namespace HanmakTechnologies.BulletinBoard.Models
{
  public class Category
  {
    public Category()
    {
      //this.jointEntitis = new HashSet<CategoryBulletin>();
    }
    public int CategoryID { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

    //public virtual ICollection<BulletinCategory> JointEntities { get; set; }

  }
}