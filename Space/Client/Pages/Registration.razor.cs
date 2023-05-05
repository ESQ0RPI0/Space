using Space.Forms.Registration;

namespace Space.Client.Pages
{
    public partial class Registration
    {
        private UserRegistrationForm userRegistrationForm = new UserRegistrationForm();
        public async Task RegisterAsync()
        {
            var result = await httpClient.Post<bool, UserRegistrationForm>("url", userRegistrationForm);
        }
    }
}