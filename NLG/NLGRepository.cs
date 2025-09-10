using Models;

namespace NLG
{
    public class NLGRepository : INLGRepository
    {
        public NLGResult GenerateDefault()
        {
            return new NLGResult{ Result = "Undefined", Message = "Im sorry i didnt quite understand. Can you rephrase?"}; 
        }
    }
}
