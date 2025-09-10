namespace BlazorUI.Pages
{
    partial class Chat
    {

        private List<string> Messages = new();

        private void HandleMessageSent(string message)
        {
            Messages.Add(message);
        }
    }
}
