namespace NLU
{
    public class NLUClass
    {
        public string HandleUserInput(string input)
        {
            var tokenized = Tokenize(input);
            Console.WriteLine(tokenized);
            if (input.Contains("Hello"))
                return "Hello how can i help??";

            if (input.Contains("dog"))
                return "woofer";
            return "Det forstod jeg ikke.";
        }
        public static string[] Tokenize(string input)
        {
            return input
                .ToLower()
                .Split(new[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
