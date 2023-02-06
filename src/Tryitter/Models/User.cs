using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tryitter.Models;

[Table("Users")]
public class User : EntityBase
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public string Module { get; set; }
    public string Status { get; set; }

    [MinLength(6)]
    public string Password { get; set; }

    public List<Post>? Posts { get; set; }
    
    // public List<User> Follow { get; set; }
    // public List<User> Following { get; set; }
}