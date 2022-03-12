using CadastroApi.Models;
using System.Security.Claims;

namespace CadastroApi.Extensions
{
    public static class UserClaimsExtension
    {
        public static List<Claim> GetClaims(this User user)
        {
            var results = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };

            return results;
        }
    }
}
