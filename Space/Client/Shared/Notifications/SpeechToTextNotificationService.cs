namespace Space.Client.Shared.Notifications;

public class SpeechToTextNotificationService
{
    public event EventHandler? EventClick;

    public void NotifyEventClick(object sender)
    {
        if (EventClick != null)
        {
            EventClick(sender, EventArgs.Empty);
        }
    }
}