using Microsoft.AspNetCore.Components;
using MudBlazor;
using Space.Forms.Registration;

namespace Space.Client.Pages
{
    public partial class Registration
    {
        private UserRegistrationForm userRegistrationForm = new UserRegistrationForm();

  
        public async Task RegisterAsync()
        {
            var result = await httpClient.Post<bool, UserRegistrationForm>("https://localhost:7272/api/registration", userRegistrationForm);


        }
    }
}