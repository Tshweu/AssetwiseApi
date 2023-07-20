using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetwiseApi.Models;

public class ServiceDocument{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public string Url { get; set; }
    public DateTime DateUploaded { get; set; }
    public User User { get; set; }
    public Service Service { get; set; }
}