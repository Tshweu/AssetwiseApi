using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetwiseApi.Models;

public class Company{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public string Name { get; set; }
    public string LogoUrl { get; set; }
    public string TradingAs { get; set; }
    public string Registration { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string Country { get; set; }
    public string VATNumber { get; set; }
    public decimal quoteTrigger { get; set; } = 5000M;
    public List<User> Users { get; set; }
    public List<Site> Sites { get; set; }
    public List<ServiceReminder> ServiceReminders { get; set; }
    public List<Service> Services { get; set; }

}