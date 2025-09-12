using ChatBot.Interfaces;
using ChatBot.Repositories;
using Models;
using System.Text.Json;

namespace BlazorUI.Services
{
    public class ChatSession(IDialogManager dialogManager)
    {
        private string _currentState = "Start";

        public List<ChatMessage> Messages { get; } = new();


        public void Reset()
        {
            _currentState = "Start";
            Messages.Clear();
        }

        public void Send(string userInput)
        {
            // Tilføj brugerens besked til historikken
            Messages.Add(new ChatMessage
            {
                Text = userInput,
                IsUser = true
            });

            // Hent svar fra dialogmanageren
            var response = dialogManager.HandleUserInput(userInput, _currentState);

            Messages.Add(new ChatMessage
            {
                Text = JsonSerializer.Serialize(response),
                IsUser = false
            });
        }
    }
}
