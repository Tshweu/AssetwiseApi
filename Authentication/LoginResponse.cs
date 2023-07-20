using Microsoft.AspNetCore.Identity;

namespace AssetwiseApi.Authentication;
    public class LoginResponse
    {
        public required string JwtToken { get; set; }
        public DateTime Expiration { get; set; }
        public List<string> Roles { get; set; }
    }