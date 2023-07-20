using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AssetwiseApi.Models;

public class User : IdentityUser{
    public string Name { get; set; }
    public string Surname { get; set; }
    public bool IsActivated { get; set; } = true;
    public Company Company { get; set; }
    public List<Site> Sites { get; set; }
    //Assign user to specific building/s
    // public User(string Name,string Surname,string PhoneNumber, string Email,string Password){
    //     this.Name = Name;
    //     this.Surname = Surname;
    //     this.PhoneNumber = PhoneNumber;
    //     this.Email = Email;
    //     this.Password = Password;
    // }
}