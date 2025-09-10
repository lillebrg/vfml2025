using Models;

namespace NLU
{
    public interface INLURepository
    {
        Task<BotModel> Get(string input);
    }
}
