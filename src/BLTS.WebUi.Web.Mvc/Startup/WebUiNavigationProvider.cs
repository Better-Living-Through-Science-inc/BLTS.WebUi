using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using BLTS.WebUi.Authorization;

namespace BLTS.WebUi.Web.Startup
{
  /// <summary>
  /// This class defines menus for the application.
  /// </summary>
  public class WebUiNavigationProvider : NavigationProvider
  {
    public override void SetNavigation(INavigationProviderContext context)
    {
      context.Manager.MainMenu
          .AddItem(new MenuItemDefinition(name: "Home",
                                          displayName: new FixedLocalizableString("Home Page"),
                                          url: "",
                                          icon: "fas fa-home",
                                          requiresAuthentication: false,
                                          order: 1
                                          )
          )

      #region UserPortal
          .AddItem(new MenuItemDefinition(name: "UserPortal",
                                          displayName: new FixedLocalizableString("User Portal"),
                                          icon: "fas fa-id-badge",
                                          requiresAuthentication: true,
                                          order: 2
                                          )
                                          .AddItem(new MenuItemDefinition(name: "CampusInfo",
                                                                          displayName: new FixedLocalizableString("Campus Information"),
                                                                          url: "About/CampusInfo",
                                                                          icon: "fas fa-wifi",
                                                                          requiresAuthentication: true,
                                                                          order: 1
                                                                          )
                                          )
                                          .AddItem(new MenuItemDefinition(name: "CampusMap",
                                                                          displayName: new FixedLocalizableString("Campus Map"),
                                                                          url: "About/CampusMap",
                                                                          icon: "fas fa-map-marked-alt",
                                                                          requiresAuthentication: true,
                                                                          order: 2
                                                                          )
                                          )
                                          .AddItem(new MenuItemDefinition(name: "ChangePassword",
                                                                          displayName: new FixedLocalizableString("Change Password"),
                                                                          url: "Users/ChangePassword",
                                                                          icon: "fas fa-key",
                                                                          requiresAuthentication: true,
                                                                          order: 3
                                                                          )
                                          )
                                          .AddItem(new MenuItemDefinition(name: "Logout",
                                                                          displayName: new FixedLocalizableString("Logout"),
                                                                          url: "Account/Logout",
                                                                          icon: "fas fa-sign-out-alt",
                                                                          requiresAuthentication: true,
                                                                          order: 4
                                                                          )
                                          )
          )
      #endregion

      #region Administration
          .AddItem(new MenuItemDefinition(name: "Administration",
                                          displayName: new FixedLocalizableString("Administration"),
                                          icon: "fas fa-id-card",
                                          permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users),
                                          order: 3
                                          )
                                          .AddItem(new MenuItemDefinition("Users",
                                                                          displayName: new FixedLocalizableString("Users"),
                                                                          url: "Users",
                                                                          icon: "fas fa-users",
                                                                          permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users),
                                                                          order: 1
                                                                          )
                                          )
                                          .AddItem(new MenuItemDefinition("Roles",
                                                                          displayName: new FixedLocalizableString("Roles"),
                                                                          url: "Roles",
                                                                          icon: "fas fa-theater-masks",
                                                                          permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Roles),
                                                                          order: 2
                                                                          )
                                          )
                                          .AddItem(new MenuItemDefinition("Tenants",
                                                                          displayName: new FixedLocalizableString("Tenants"),
                                                                          url: "Tenants",
                                                                          icon: "fas fa-building",
                                                                          permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Tenants),
                                                                          order: 3
                                                                          )
                                          )
          )
      #endregion

      #region ResearchInitiative
          .AddItem(new MenuItemDefinition(name: "ResearchInitiative",
                                          displayName: new FixedLocalizableString("Research Initiative"),
                                          icon: "fas fa-book",
                                          requiresAuthentication: false,
                                          order: 4
                                          )
                                          .AddItem(new MenuItemDefinition(name: "ResearchPhilosophy",
                                                                          displayName: new FixedLocalizableString("Research Philosophy"),
                                                                          url: "Research",
                                                                          icon: "fas fa-star",
                                                                          requiresAuthentication: false,
                                                                          order: 1
                                                                          )
                                          )
                                          .AddItem(new MenuItemDefinition(name: "AtomicManufacturing",
                                                                          displayName: new FixedLocalizableString("Atomic Manufacturing"),
                                                                          url: "Research/Atomic",
                                                                          icon: "fas fa-atom",
                                                                          requiresAuthentication: false,
                                                                          order: 2
                                                                          )
                                          )
                                          .AddItem(new MenuItemDefinition(name: "Biophysics",
                                                                          displayName: new FixedLocalizableString("Biophysics"),
                                                                          url: "Research/Biophysics",
                                                                          icon: "fas fa-dna",
                                                                          requiresAuthentication: false,
                                                                          order: 3
                                                                          )
                                          )
                                          .AddItem(new MenuItemDefinition(name: "HardwareEngineering",
                                                                          displayName: new FixedLocalizableString("Hardware Engineering"),
                                                                          url: "Research/Hardware",
                                                                          icon: "fas fa-microchip",
                                                                          requiresAuthentication: false,
                                                                          order: 4
                                                                          )
                                          )
                                          .AddItem(new MenuItemDefinition(name: "Neurophysics",
                                                                          displayName: new FixedLocalizableString("Neurophysics"),
                                                                          url: "Research/Neurophysics",
                                                                          icon: "fas fa-brain",
                                                                          requiresAuthentication: false,
                                                                          order: 5
                                                                          )
                                          )
                                          .AddItem(new MenuItemDefinition(name: "SoftwareEngineering",
                                                                          displayName: new FixedLocalizableString("Software Engineering"),
                                                                          url: "Research/Software",
                                                                          icon: "fas fa-hat-wizard",
                                                                          requiresAuthentication: false,
                                                                          order: 6
                                                                          )
                                          )
          )
      #endregion

      #region OutreachPrograms
          .AddItem(new MenuItemDefinition(name: "CommunityOutreach",
                                          displayName: new FixedLocalizableString("Community Outreach"),
                                          icon: "fas fa-hand-holding-heart",
                                          requiresAuthentication: false,
                                          order: 5
                                          )
                                          .AddItem(new MenuItemDefinition(name: "OutreachPhilosophy",
                                                                          displayName: new FixedLocalizableString("Outreach Philosophy"),
                                                                          url: "Outreach",
                                                                          icon: "fas fa-heart",
                                                                          requiresAuthentication: false,
                                                                          order: 1
                                                                          )
                                          )
                                          .AddItem(new MenuItemDefinition(name: "AfterSchoolPrograms",
                                                                          displayName: new FixedLocalizableString("After School Programs"),
                                                                          url: "Outreach/AfterSchool",
                                                                          icon: "fas fa-theater-masks",
                                                                          requiresAuthentication: false,
                                                                          order: 2
                                                                          )
                                          )
                                          .AddItem(new MenuItemDefinition(name: "EducationalFacilities",
                                                                          displayName: new FixedLocalizableString("Educational Facilities"),
                                                                          url: "Outreach/EducationalFacility",
                                                                          icon: "fas fa-school",
                                                                          requiresAuthentication: false,
                                                                          order: 3
                                                                          )
                                          )
                                          .AddItem(new MenuItemDefinition(name: "EducationalSupplies",
                                                                          displayName: new FixedLocalizableString("Educational Supplies"),
                                                                          url: "Outreach/EducationalSupply",
                                                                          icon: "fas fa-book-reader",
                                                                          requiresAuthentication: false,
                                                                          order: 4
                                                                          )
                                          )
                                          .AddItem(new MenuItemDefinition(name: "StudentLunchProgram",
                                                                          displayName: new FixedLocalizableString("Student Lunch Program"),
                                                                          url: "Outreach/StudentLunch",
                                                                          icon: "fas fa-utensils",
                                                                          requiresAuthentication: false,
                                                                          order: 5
                                                                          )
                                          )
                                          .AddItem(new MenuItemDefinition(name: "TeacherRecruitment",
                                                                          displayName: new FixedLocalizableString("Teacher Recruitment"),
                                                                          url: "Outreach/TeacherRecruitment",
                                                                          icon: "fas fa-chalkboard-teacher",
                                                                          requiresAuthentication: false,
                                                                          order: 6
                                                                          )
                                          )
                                          .AddItem(new MenuItemDefinition(name: "VaccineProgram",
                                                                          displayName: new FixedLocalizableString("Vaccine Program"),
                                                                          url: "Outreach/Vaccine",
                                                                          icon: "fas fa-syringe",
                                                                          requiresAuthentication: false,
                                                                          order: 7
                                                                          )
                                          )
          )
      #endregion

      #region About
          .AddItem(new MenuItemDefinition(name: "Information",
                                          displayName: new FixedLocalizableString("Information"),
                                          icon: "fas fa-info-circle",
                                          requiresAuthentication: false,
                                          order: 6
                                          )
                                          .AddItem(new MenuItemDefinition(name: "About",
                                                                          displayName: new FixedLocalizableString("About"),
                                                                          url: "About",
                                                                          icon: "fas fa-question-circle",
                                                                          requiresAuthentication: false,
                                                                          order: 1
                                                                          )
                                          )
                                          .AddItem(new MenuItemDefinition(name: "Budget",
                                                                          displayName: new FixedLocalizableString("Budget"),
                                                                          url: "About/Budget",
                                                                          icon: "fas fa-file-invoice-dollar",
                                                                          requiresAuthentication: false,
                                                                          order: 2
                                                                          )
                                          )
                                          .AddItem(new MenuItemDefinition(name: "Contact",
                                                                          displayName: new FixedLocalizableString("Contact"),
                                                                          url: "About/Contact",
                                                                          icon: "fas fa-address-card",
                                                                          requiresAuthentication: false,
                                                                          order: 3
                                                                          )
                                          )
                                          .AddItem(new MenuItemDefinition(name: "CoreValues",
                                                                          displayName: new FixedLocalizableString("Core Values"),
                                                                          url: "About/CoreValues",
                                                                          icon: "fas fa-star",
                                                                          requiresAuthentication: false,
                                                                          order: 4
                                                                          )
                                          )
                                          .AddItem(new MenuItemDefinition(name: "EmployeeRecruitment",
                                                                          displayName: new FixedLocalizableString("Employee Recruitment"),
                                                                          url: "About/EmployeeRecruitment",
                                                                          icon: "fas fa-pizza-slice",
                                                                          requiresAuthentication: false,
                                                                          order: 5
                                                                          )
                                          )
                                          .AddItem(new MenuItemDefinition(name: "Organization",
                                                                          displayName: new FixedLocalizableString("Organization"),
                                                                          url: "About/Organization",
                                                                          icon: "fas fa-sitemap",
                                                                          requiresAuthentication: false,
                                                                          order: 6
                                                                          )
                                          )
          )
      #endregion
      ;
    }

    private static ILocalizableString L(string name)
    {
      return new LocalizableString(name, WebUiConsts.LocalizationSourceName);
    }
  }
}
