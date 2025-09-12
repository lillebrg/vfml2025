using Models;

namespace NLG
{
    public interface INLGRepository
    {
        string GenerateResponse(SessionState state);
        string GenerateDefault();
    }
}
