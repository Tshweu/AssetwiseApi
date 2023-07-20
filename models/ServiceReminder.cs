using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AssetwiseApi.Constants;
namespace AssetwiseApi.Models;
public class ServiceReminder{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public ServiceType Type { get; set; }
    public ServicePriority Priority { get; set; }
    public List<Asset> Assets { get; set; }
    public Site Site{ get; set; }
    public Company Company { get; set; }
    public DateTime DueDate { get; set; }
    public ServiceReminderStatus Status { get; set; }
}