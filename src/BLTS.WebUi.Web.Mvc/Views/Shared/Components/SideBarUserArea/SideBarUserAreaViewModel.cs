using BLTS.WebUi.Sessions.Dto;

namespace BLTS.WebUi.Web.Views.Shared.Components.SideBarUserArea
{
  public class SideBarUserAreaViewModel
  {
    public GetCurrentLoginInformationsOutput LoginInformations { get; set; }

    public bool IsMultiTenancyEnabled { get; set; }

    public string GetShownLoginName()
    {
      string userName = IsUserLoggedIn() ? LoginInformations.User.Surname + ", " + LoginInformations.User.Name : "Guest";

      if (!IsMultiTenancyEnabled)
        return userName;
      else
        return LoginInformations.Tenant == null
            ? userName
            : LoginInformations.Tenant.TenancyName + " - " + userName;
    }

    public bool IsUserLoggedIn()
    {
      return LoginInformations.User != null;
    }
  }
}
