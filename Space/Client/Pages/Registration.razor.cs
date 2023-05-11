using Microsoft.AspNetCore.Components;
using MudBlazor;
using Space.Forms.Registration;

namespace Space.Client.Pages
{
    public partial class Registration
    {
        MudForm form;
        bool success;
        string[] errors = { };
        private UserRegistrationForm userRegistrationForm = new UserRegistrationForm();

  
        public async Task RegisterAsync()
        {
            var result = await httpClient.Post<string, UserRegistrationForm>("https://localhost:7272/api/Registration/Register", userRegistrationForm);


        }
    }
}