using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tryitter.Models;
[Table("Posts")]
public class Post : EntityBase
{
    [Key]
    public int Id { get; set; }
    [MaxLength(300)]
    public string Message { get; set; }
   

    public int? UserId  { get; set; }
    public User User { get; set; }

    // public List<Post> ChildMessages { get; set; }

}