using BLTS.WebUi.Configurations;
using BLTS.WebUi.Logs;
using Microsoft.Identity.Web;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLTS.WebUi.UserManagers
{
    public class UserAuthenticationManager
    {
        private readonly IApplicationLogTools _applicationLogTools;
        private readonly ConfigurationManager _configurationManager;
        private readonly ITokenAcquisition _tokenAcquisition;

        public UserAuthenticationManager(IApplicationLogTools applicationLogTools
                                       , ConfigurationManager configurationManager
                                       , ITokenAcquisition tokenAcquisition)
        {
            _applicationLogTools = applicationLogTools;
            _configurationManager = configurationManager;
            _tokenAcquisition = tokenAcquisition;
        }

        /// <summary>
        /// retrieve the Auth Token for the current user
        /// </summary>
        /// <param name="apiGroupConfigurationName"></param>
        /// <returns></returns>
        public async Task<string> GetAccessToken(string apiGroupConfigurationName = "ApiUser")
        {
            try
            {
                return await _tokenAcquisition.GetAccessTokenForUserAsync(new List<string>() { _configurationManager.GetValue($"AzureAdPermissions:{apiGroupConfigurationName}Scope") });
            }
            catch //(Exception authenticationError)
            {
                //expected that this happens when the user login has expired but the identity is known, no need to log
                //_applicationLogTools.LogError(authenticationError, new Dictionary<string, dynamic> { { "ClassName", $"WebApi.ApiControllerBase" } });
                return null;
            }
        }

        /// <summary>
        /// checks to see if Microsoft Identity will return a token for a fully authenticated user, if false then user must process normal identity verification for full access
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsUserFullyAuthenticated(string apiGroupConfigurationName = "ApiUser")
        {
            return (string.IsNullOrWhiteSpace(await GetAccessToken(apiGroupConfigurationName)));
        }

        public string GetUserFullName(ClaimsPrincipal user)
        {

            return user.Claims.Where(x => x.Type.Equals("name")).FirstOrDefault().Value;
        }

    }
}
