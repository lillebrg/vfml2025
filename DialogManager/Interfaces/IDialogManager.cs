using Models;

namespace ChatBot.Interfaces
{
    public interface IDialogManager
    {
        NLGResult HandleUserInput(string input);
    }
}
