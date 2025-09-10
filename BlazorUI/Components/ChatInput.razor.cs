using Microsoft.AspNetCore.Components.Web;
using NLU;
namespace BlazorUI.Components
{
    partial class ChatInput
    {

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

        private Task ProcessInput(string input)
        {
            NLUClass cawd = new NLUClass();
            string skibidi = cawd.HandleUserInput(input);
            Console.WriteLine($"User wrote: {skibidi}");
            return Task.CompletedTask;
        }
    }
}
