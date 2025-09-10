using ChatBot.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using NLU;
namespace BlazorUI.Components
{
    partial class ChatInput(IDialogManager dialogManager)
    {
        [Parameter] public EventCallback<string> OnMessageSent { get; set; }
        private string UserInput { get; set; } = "";
        private bool IsSubmitted { get; set; } = false;
        private async Task HandleKeyDown(KeyboardEventArgs e)
        {
            if (e.Key == "Enter" && !IsSubmitted)
            {
                IsSubmitted = true;

                await ProcessInput(UserInput);
            }
        }

        private async Task ProcessInput(string input)
        {
            var response = dialogManager.HandleUserInput(input);
            Console.WriteLine($"User wrote: {response}");
            await OnMessageSent.InvokeAsync(response.Message);
            IsSubmitted = false;
            UserInput = "";
        }
    }
}
