using ChatBot.Interfaces;
using Models;
using NLG;
using NLU;

namespace ChatBot.Repositories;

public class DialogManager(INLURepository nLURepository, INLGRepository nLGRepository, IFAQRepository fAQRepository) : IDialogManager
{
    private readonly Dictionary<string, SessionState> _sessions = new();
    public SessionState HandleUserInput(string userInput, string sessionId)
    {
        if (!_sessions.ContainsKey(sessionId))
        {
            //First time use the Intent analysis
            var nluResult = nLURepository.Predict(userInput);
            _sessions[sessionId] = new SessionState
            {
                CurrentIntent = nluResult.Intent,
                CollectedEntities = new Dictionary<string, string>(nluResult.Entities),
                CurrentStep = "Start"
            };
        }

        var state = _sessions[sessionId];
        var intent = state.CurrentIntent;
        var entities = state.CollectedEntities;

        // STATE MACHINE pr intent
        switch (intent)
        {
            case "GetAnimal":
                return fAQRepository.GetAnimalData(state, userInput);
            case "Physical":
                return fAQRepository.HandlePhysicalIntent(state, userInput);
            case "Mental":
                return fAQRepository.HandleMentalIntent(state, userInput);
            default:
                state.CurrentStep = "Unknown";
                return state;
        }
    }


}
