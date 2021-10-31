
using System.Collections.Generic;

namespace HanmakTechnologies.BulletinBoard
{
  public class Category
  {
    public Category()
    {
      this.Bulletins = new HashSet<Bulletin>();
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

    public virtual ICollection<Bulletin> Bulletins { get; set; }

  }
}