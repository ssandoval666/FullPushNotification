#pragma checksum "C:\Users\Sebastian Sandoval\Documents\GitHub\FullPushNotification\PushNotificationClient\PushNotificationClient\Shared\MainLayout.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "dfb6831c4edb77a29f1f1ca9b87ae2d13fd7376e"
// <auto-generated/>
#pragma warning disable 1591
namespace PushNotificationClient.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\Sebastian Sandoval\Documents\GitHub\FullPushNotification\PushNotificationClient\PushNotificationClient\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Sebastian Sandoval\Documents\GitHub\FullPushNotification\PushNotificationClient\PushNotificationClient\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Sebastian Sandoval\Documents\GitHub\FullPushNotification\PushNotificationClient\PushNotificationClient\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Sebastian Sandoval\Documents\GitHub\FullPushNotification\PushNotificationClient\PushNotificationClient\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Sebastian Sandoval\Documents\GitHub\FullPushNotification\PushNotificationClient\PushNotificationClient\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Sebastian Sandoval\Documents\GitHub\FullPushNotification\PushNotificationClient\PushNotificationClient\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Sebastian Sandoval\Documents\GitHub\FullPushNotification\PushNotificationClient\PushNotificationClient\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Sebastian Sandoval\Documents\GitHub\FullPushNotification\PushNotificationClient\PushNotificationClient\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\Sebastian Sandoval\Documents\GitHub\FullPushNotification\PushNotificationClient\PushNotificationClient\_Imports.razor"
using PushNotificationClient;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\Sebastian Sandoval\Documents\GitHub\FullPushNotification\PushNotificationClient\PushNotificationClient\_Imports.razor"
using PushNotificationClient.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Sebastian Sandoval\Documents\GitHub\FullPushNotification\PushNotificationClient\PushNotificationClient\Shared\MainLayout.razor"
using PushNotificationClient.Class;

#line default
#line hidden
#nullable disable
    public partial class MainLayout : LayoutComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "page");
            __builder.AddAttribute(2, "b-28u177ephz");
            __builder.OpenElement(3, "div");
            __builder.AddAttribute(4, "class", "sidebar");
            __builder.AddAttribute(5, "b-28u177ephz");
            __builder.OpenComponent<PushNotificationClient.Shared.NavMenu>(6);
            __builder.CloseComponent();
            __builder.CloseElement();
            __builder.AddMarkupContent(7, "\r\n\r\n    ");
            __builder.OpenElement(8, "div");
            __builder.AddAttribute(9, "class", "main");
            __builder.AddAttribute(10, "b-28u177ephz");
            __builder.AddMarkupContent(11, "<div class=\"top-row px-4\" b-28u177ephz><a href=\"http://blazor.net\" target=\"_blank\" class=\"ml-md-auto\" b-28u177ephz>About</a></div>\r\n\r\n        ");
            __builder.OpenElement(12, "div");
            __builder.AddAttribute(13, "class", "content px-4");
            __builder.AddAttribute(14, "b-28u177ephz");
            __builder.AddContent(15, 
#nullable restore
#line 19 "C:\Users\Sebastian Sandoval\Documents\GitHub\FullPushNotification\PushNotificationClient\PushNotificationClient\Shared\MainLayout.razor"
             Body

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 25 "C:\Users\Sebastian Sandoval\Documents\GitHub\FullPushNotification\PushNotificationClient\PushNotificationClient\Shared\MainLayout.razor"
 
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);


        var IsOnLine = await localStorage.GetItemAsync<bool>("ConnectionStatus");

        if (IsOnLine)
        {
            await GetToken();
        }


        if (firstRender)
            await RequestNotificationSubscriptionAsync();

    }

    private async Task RequestNotificationSubscriptionAsync()
    {
        var subscription = await JSRuntime.InvokeAsync<NotificationSubscription>("blazoring.requestSubscription");
        if (subscription != null)
        {
            await SubscribeToNotifications(subscription);
        }
    }

    public async Task SubscribeToNotifications(NotificationSubscription subscription)
    {
        var IsOnLine = await localStorage.GetItemAsync<bool>("ConnectionStatus");

        if (IsOnLine)
        {
            var JWToken = await localStorage.GetItemAsync<string>("JWT");

            Http.DefaultRequestHeaders.Add("Authorization", "Bearer " + JWToken);
            var response = await Http.PostAsJsonAsync("api/NotificationSubscribe", subscription);
            //response.EnsureSuccessStatusCode();

        }


    }

    public async Task GetToken()
    {
        var JWToken = await localStorage.GetItemAsync<string>("JWT");

        if (JWToken == null)
        {
            UserInfo oUser = new UserInfo();

            oUser.Email = "sss@dd.com";
            oUser.Password = "eeee";

            var response = await Http.PostAsJsonAsync("api/Token", oUser);
            var message = response.Content.ReadAsStringAsync();

            await localStorage.SetItemAsync("JWT", message.Result);
        }

    }



#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient Http { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JSRuntime { get; set; }
    }
}
#pragma warning restore 1591
