using System.Threading.Tasks;
using BLTS.WebUi.Models.TokenAuth;
using BLTS.WebUi.Web.Controllers;
using Shouldly;
using Xunit;

namespace BLTS.WebUi.Web.Tests.Controllers
{
    public class HomeController_Tests: WebUiWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}