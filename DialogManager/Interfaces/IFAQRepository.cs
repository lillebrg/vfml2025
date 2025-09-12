using Models;

namespace ChatBot.Interfaces
{
    public interface IFAQRepository
    {
        SessionState GetAnimalData(SessionState state, string userInput);
        SessionState HandlePhysicalIntent(SessionState state, string userInput);
        SessionState HandleMentalIntent(SessionState state, string userInput);
    }
}
