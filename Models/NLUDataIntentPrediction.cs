using Microsoft.ML.Data;

namespace Models
{
    public class NLUDataIntentPrediction
    {
        [ColumnName("PredictedLabel")]
        public string PredictedIntent { get; set; } = "";
    }
}
