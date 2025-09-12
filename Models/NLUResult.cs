namespace Models
{
    public class NLUResult
    {
        public string Intent { get; set; }
        public Dictionary<string, string> Entities { get; set; }

        public NLUResult(string intent, Dictionary<string, string> entities = null)
        {
            Intent = intent;
            Entities = entities ?? new Dictionary<string, string>();
        }
    }
}
