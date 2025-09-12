using Microsoft.ML.Data;

namespace Models
{
    public class NLUData
    {
        [LoadColumn(0)]
        public string Text { get; set; } = "";

        [LoadColumn(1)]
        public string Intent { get; set; } = "";
    }
}
