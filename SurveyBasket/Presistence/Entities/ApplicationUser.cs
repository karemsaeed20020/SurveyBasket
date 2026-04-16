using Microsoft.AspNetCore.Identity;

namespace SurveyBasket.Presistence.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsDiable { get; set; }
        public byte[]? ProfileImage { get; set; }
        public string ProfileImageContentType { get; set; } = string.Empty;
        public List<RefreshToken> RefreshTokens { get; set; } = [];
    }
}
