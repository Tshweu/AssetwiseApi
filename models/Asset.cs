using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetwiseApi.Models;

public class Asset{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    //Consider making service plan an enum
    public int ServicePlan { get; set; }
    public bool IsActive { get; set; }
    public DateTime DateCreated { get; set; }
    //Maybe track who created the asset
    // public User CreatedBy { get; set; }
    public DateTime LastServiceDate { get; set; }
    public DateTime NextServiceDate { get; set; }
    public string? ImageUrl { get; set; }
    public Site Site { get; set; }
    public List<Service> ServiceHistory { get; set; }

}