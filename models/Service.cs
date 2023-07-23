using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AssetwiseApi.Constants;
namespace AssetwiseApi.Models;
public class Service{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    // [Column(TypeName = "nvarchar(24)")]
    public ServiceType Type { get; set; }
    // [Column(TypeName = "nvarchar(24)")]
    public ServicePriority Priority { get; set; }
    // [Column(TypeName = "nvarchar(24)")]
    public List<ServiceStatus> Status { get; set; }
    public string? Description { get; set; }
    public User CreatedBy { get; set; }
    public Site Site{ get; set; }
    public Company Company { get; set; }
    public List<ServiceDocument> Documents { get; set; }
    public List<ServiceComment> Comments { get; set; }
}