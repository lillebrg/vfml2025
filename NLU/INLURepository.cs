using Models;

namespace NLU
{
    public interface INLURepository
    {
        NLUResult Predict(string input);
    }
}
