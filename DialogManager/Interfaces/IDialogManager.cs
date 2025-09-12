using Models;

namespace ChatBot.Interfaces
{
    public interface IDialogManager
    {
        SessionState HandleUserInput(string userInput, string sessionId);
    }
}
