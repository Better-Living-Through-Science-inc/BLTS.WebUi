using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace BLTS.WebUi.Identity
{
    public class ExternalLoginInfoHelper
    {
        public static (string name, string surname) GetNameAndSurnameFromClaims(List<Claim> claims)
        {
            string name = null;
            string surname = null;

            var givennameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName);
            if (givennameClaim != null && !string.IsNullOrEmpty(givennameClaim.Value))
            {
                name = givennameClaim.Value;
            }

            var surnameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname);
            if (surnameClaim != null && !string.IsNullOrEmpty(surnameClaim.Value))
            {
                surname = surnameClaim.Value;
            }

            if (name == null || surname == null)
            {
                var nameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
                if (nameClaim != null)
                {
                    var nameSurName = nameClaim.Value;
                    if (!string.IsNullOrEmpty(nameSurName))
                    {
                        var lastSpaceIndex = nameSurName.LastIndexOf(' ');
                        if (lastSpaceIndex < 1 || lastSpaceIndex > (nameSurName.Length - 2))
                        {
                            name = surname = nameSurName;
                        }
                        else
                        {
                            name = nameSurName.Substring(0, lastSpaceIndex);
                            surname = nameSurName.Substring(lastSpaceIndex);
                        }
                    }
                }
            }

            return (name, surname);
        }
    }
}
