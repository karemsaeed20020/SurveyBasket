using SurveyBasket.Presistence.Entities;

namespace SurveyBasket.Authentication
{
    public interface IJwtProvider
    {
        (string token, int Expirein) GenerateToken(ApplicationUser user, IEnumerable<string> roles, IEnumerable<string> permissions);
        String ValidateToken(string token);
    }
}
