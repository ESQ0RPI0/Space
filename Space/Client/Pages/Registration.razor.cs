using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using Space.Client;
using Space.Client.Shared;
using Space.Forms.Registration;
using MudBlazor;

namespace Space.Client.Pages
{
    public partial class Registration
    {
        private UserRegistrationForm userRegistrationForm = new UserRegistrationForm();
        public async Task RegisterAsync()
        {
            var result = await httpClient.PostAsJsonAsync("url", userRegistrationForm);
        }
    }
}