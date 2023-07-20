using System.ComponentModel.DataAnnotations;

namespace AssetwiseApi.Authentication;
 public class RegistrationModel  
    {  
        [Required(ErrorMessage = "User Name is required")]  
        public string Username { get; set; }  

        [Required(ErrorMessage = "Surname is required")]  
        public string Name { get; set; }  

        [Required(ErrorMessage = "Name is required")]  
        public string Surname { get; set; }  

        [Required(ErrorMessage = "Phone Number is required")]  
        public string PhoneNumber { get; set; }  
        
        [EmailAddress]  
        [Required(ErrorMessage = "Email is required")]  
        public string Email { get; set; }  
  
        [Required(ErrorMessage = "Password is required")]  
        public string Password { get; set; }  
  
    }  