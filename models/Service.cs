using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AssetwiseApi.Constants;
namespace AssetwiseApi.Models;
public class Service{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public ServiceType Type { get; set; }
    public ServicePriority Priority { get; set; }
    public ServiceStatus Status { get; set; }
    public string? Description { get; set; }
    public User CreatedBy { get; set; }
    public Site Site{ get; set; }
    public Company Company { get; set; }
    public List<ServiceDocument> Documents { get; set; }
    public List<ServiceDocument> Comments { get; set; }
}