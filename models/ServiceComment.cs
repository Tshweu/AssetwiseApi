using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetwiseApi.Models;

public class ServiceComment{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public string Comment { get; set; }
    public DateTime DateCreated { get; set; }
    public User User { get; set; }
    public Service Service { get; set; }
}