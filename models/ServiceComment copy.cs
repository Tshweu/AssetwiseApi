using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AssetwiseApi.Constants;

namespace AssetwiseApi.Models;

public class ServiceStatus{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public ServiceStatus Status { get; set; }
    public DateTime DateCreated { get; set; }
    public User User { get; set; }
    public Service Service { get; set; }
}