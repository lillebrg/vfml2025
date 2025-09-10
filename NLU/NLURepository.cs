using Models;
using System.Text.RegularExpressions;

namespace NLU
{
    public class NLURepository() : INLURepository
    {
        private static readonly string[] FAQKeywords = ["trick", "nail", "tail", "nose"];
        private static readonly string[] EmergencyKeywords = ["dying", "chok", "puk", "sick"];

        private static bool ContainsAny(IEnumerable<string> tokens, string[] keywords) =>
       tokens.Any(t => keywords.Contains(t));
        public Task<BotModel> Get(string input)
        {
            input = input.ToLower();
            var tokens = Tokenize(input);
            var stemmedTokens = tokens.Select(Stem).ToList();
            BotState state;
            Animal animal = GetAnimal(input);
            if (ContainsAny(tokens, FAQKeywords))
                state = BotState.FAQ;
            else if (ContainsAny(tokens, EmergencyKeywords))
                state = BotState.Emergency;
            else
                state = BotState.Idle;
            return Task.FromResult(new BotModel { Animal = animal, BotState = state });
        }

        private Animal GetAnimal(string input)
        {
            Animal animal = new Animal();
            // Entity: Animal (dog, cat, rabbit, seagull, etc.)
            var animalMatch = Regex.Match(input, @"\b(dog|cat|rabbit|hamster|parrot|seagull|horse)\b", RegexOptions.IgnoreCase);
            if (!animalMatch.Success)
                animal.Name = animalMatch.Groups[1].Value;
            // Entity: Breed (look for "breed is X" or "is a X")
            var breedMatch = Regex.Match(input, @"(?:breed\s+is|is\s+a|is\s+an)\s+([A-Za-zæøå\s]+)", RegexOptions.IgnoreCase);
            if (breedMatch.Success)
                animal.Breed = breedMatch.Groups[1].Value;

            // Entity: Age (e.g. "3 years old" or "1 year old")
            var ageMatch = Regex.Match(input, @"(\d+)\s+(?:year)\s+old", RegexOptions.IgnoreCase);
            if (ageMatch.Success)
                animal.Age = breedMatch.Groups[1].Value;

            return animal;

        }

        private static List<string> Tokenize(string input)
        {
            return input
                .ToLower()
                .Split([' ', ',', '.', '?', '!', ';'], StringSplitOptions.RemoveEmptyEntries)
                .ToList();
        }

        private static string Stem(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return word;

            word = word.ToLower();

            if (word.EndsWith("ing"))
                return word.Substring(0, word.Length - 3);

            else if (word.EndsWith("es") && word.Length > 3)
            {
                // prevent cutting off words like "xylitos" -> "xylito"
                // only stem if word before 's' is a vowel/consonant that makes sense
                return word.Substring(0, word.Length - 2);
            }
            else if (word.EndsWith("s") && word.Length > 3)
            {
                // prevent cutting off words like "xylitos" -> "xylito"
                // only stem if word before 's' is a vowel/consonant that makes sense
                return word.Substring(0, word.Length - 1);
            }

            return word;
        }
    }
}

