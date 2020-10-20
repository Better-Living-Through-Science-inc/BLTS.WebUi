
namespace BLTS.WebUi.Pages.Shared.Components.SideBarUserArea
{
    public class SideBarUserAreaViewModel
    {
        public GetCurrentLoginInformationsOutput LoginInformations { get; set; }

        public bool IsMultiTenancyEnabled { get; set; }

        public string GetShownLoginName()
        {
            string userName = IsUserLoggedIn() ? LoginInformations.User.Surname + ", " + LoginInformations.User.Name : "Guest";

            return userName;
        }

        public bool IsUserLoggedIn()
        {
            return false; //LoginInformations.User != null;
        }
    }

    public class GetCurrentLoginInformationsOutput
    {
        public User User { get; internal set; }
    }

    public class User
    {
        public string Surname { get; internal set; }
        public string Name { get; internal set; }
    }
}
