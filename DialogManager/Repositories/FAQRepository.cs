using Models;
using ChatBot.Interfaces;

namespace ChatBot.Repositories
{
    public class FAQRepository : IFAQRepository
    {
        public SessionState GetAnimalData(SessionState state, string userInput)
        {
            var step = state.CurrentStep;

            if (step == "Start")
            {
                state.CurrentStep = "AskBreed";
            }
            else if (step == "AskBreed")
            {
                state.CollectedEntities["Breed"] = userInput;
                state.CurrentStep = "AskAge";
            }
            else if (step == "AskAge")
            {
                state.CollectedEntities["Age"] = userInput;
                state.CurrentStep = "AskGuests";
            }

            return state;
        }

        public SessionState HandleMentalIntent(SessionState state, string userInput)
        {
            throw new NotImplementedException();

        }

        public SessionState HandlePhysicalIntent(SessionState state, string userInput)
        {
            throw new NotImplementedException();
        }
    }
}
