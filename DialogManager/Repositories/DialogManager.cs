using ChatBot.Interfaces;
using Models;
using NLG;
using NLU;

namespace ChatBot.Repositories;

public class DialogManager(INLURepository nLURepository, INLGRepository nLGRepository, IFAQRepository fAQRepository, IEmergencyRespository emergencyRespository) : IDialogManager
{
    public NLGResult HandleUserInput(string input)
    {
        var result = nLURepository.Get(input);
        
        if (result.IsCompleted)
        {
            return new NLGResult { Result = "Undefined", Message = "Nothing Implemented" };
        }

        else
        {

            return nLGRepository.GenerateDefault();
        }
    }
}
