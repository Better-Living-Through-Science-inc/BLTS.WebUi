﻿using BLTS.WebUi.Sessions.Dto;

namespace BLTS.WebUi.Web.Views.Shared.Components.SideBarUserArea
{
    public class SideBarUserAreaViewModel
    {
        public GetCurrentLoginInformationsOutput LoginInformations { get; set; }

        public bool IsMultiTenancyEnabled { get; set; }

        public string GetShownLoginName()
        {
            string userName = LoginInformations.User != null ? LoginInformations.User.UserName : "Guest";

            if (!IsMultiTenancyEnabled)
            {
                return userName;
            }

            return LoginInformations.Tenant == null
                ? userName
                : LoginInformations.Tenant.TenancyName + " - " + userName;
        }
    }
}
