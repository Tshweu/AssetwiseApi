namespace AssetwiseApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Site{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string AddressLine1 { get; set; }
    //ToDo: Add other address details
    //
    public Company Company { get; set; }
    public List<User> Users { get; set; }
}