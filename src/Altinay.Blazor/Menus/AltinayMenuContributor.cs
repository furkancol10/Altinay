using System.Threading.Tasks;
using Altinay.Localization;
using Altinay.MultiTenancy;
using Volo.Abp.Identity.Blazor;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.UI.Navigation;

namespace Altinay.Blazor.Menus;

public class AltinayMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<AltinayResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                AltinayMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home",
                order: 0
            )
        );

        context.Menu.AddItem(
        new ApplicationMenuItem(
            AltinayMenus.PersonelRequests,
            l["Menu:PersonelRequests"],
            "/personel-requests",
            icon: "fas fa-users"
            )
        );

        context.Menu.AddItem(
        new ApplicationMenuItem(
            AltinayMenus.MeetingRoomBooking,
            l["Menu:MeetingBooking"],
            "/meeting-bookings",
            icon: "fas fa-calendar"
        )
    );

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);

        return Task.CompletedTask;
    }
}
