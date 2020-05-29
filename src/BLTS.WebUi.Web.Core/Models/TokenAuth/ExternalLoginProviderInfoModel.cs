using Abp.AutoMapper;
using BLTS.WebUi.Authentication.External;

namespace BLTS.WebUi.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
