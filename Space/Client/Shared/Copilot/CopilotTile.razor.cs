using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using MudBlazor;
using Space.Client.AI.Core.Setup;
using Space.Client.Shared.Notifications;

namespace Space.Client.Shared.Copilot;

public partial class CopilotTile
{
    [Inject] 
    private SpeechToTextNotificationService _sttService { get; set; }
    [Inject] 
    private IConfiguration _configuration { get; set; }
    [Inject]
    private ICopilotAgent _copilotHelper { get; set; }

    [Parameter]
    public EventCallback<string> PromptCallback { get; set; }


    private MudTextField<string> InputFieldRef { get; set; }
    public string Prompt { get; set; }
    public string TTSKey { get; set; }
    public string TTSRegion { get; set; }
    public string TTSLanguage { get; set; }

    protected override void OnInitialized()
    {
        TTSKey = _configuration.GetValue<string>("SPEECH_API_KEY");
        TTSRegion = _configuration.GetValue<string>("SPEECH_REGION");
        TTSLanguage = _configuration.GetValue<string>("SPEECH_LANGUAGE");
    }

    public async void ExecutePrompt()
    {
        var prompt = Prompt;
        Prompt = $"Loading answer for '{prompt}'...";
        await PromptCallback.InvokeAsync(prompt);

        StateHasChanged();
    }

    public async void ExecutePromptOnKeyDown(KeyboardEventArgs args)
    {
        if (args.Key == "Enter")
        {
            await InputFieldRef.BlurAsync();
            await Task.Delay(100);
            StateHasChanged();
            Prompt = Prompt.Trim();
            StateHasChanged();
            ExecutePrompt();
            await InputFieldRef!.FocusAsync();
        }
    }
}