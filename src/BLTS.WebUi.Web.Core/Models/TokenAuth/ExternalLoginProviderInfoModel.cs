
using AutoMapper;

namespace BLTS.WebUi.Models.TokenAuth
{
    //[AutoMap(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
