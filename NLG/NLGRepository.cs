using Models;

namespace NLG
{
    public class NLGRepository : INLGRepository
    {
        public string GenerateResponse(SessionState state)
        {
            var step = state.CurrentStep;
            var entities = state.CollectedEntities;



            entities.TryGetValue("Breed", out var breed);
            breed ??= "UknownBreed";

            entities.TryGetValue("Age", out var age);
            age ??= "UknownAge";

            return state.CurrentIntent switch
            {
                "getAnimal" => step switch
                {
                    "AskBreed" => $"First i need to know what kind of breed your dog is?",
                    "AskAge" => $" And how old is your {breed}",
                    _ => "I didnt quite understand. Can you say it in a different way?"
                },
                "FAQ" => step switch
                {
                    "NoEating" => $"If your dog is not eating, it could be a lot of things. i have listed some here: Old age, stress, depression, the smell of the food, stomach issues etc. Remeber different breed dogs are different too. Your {breed} might have different needs compared to other, this is just general information",
                    "Command" => $"The number one priority when training your {breed}, give it a treat when done correctly. An important thing afterwards, is to stick to your command if your dog tries to not do it. if they get away with it it resets all your progress with your {breed}",
                    _ => "Beklager, noget gik galt."
                },
                _ => GenerateDefault()
            };
        }
        public string GenerateDefault()
        {
            return "Im sorry i didnt quite understand. Can you rephrase?"; 
        }
    }
}
