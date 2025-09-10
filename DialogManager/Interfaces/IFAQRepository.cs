using Models;

namespace ChatBot.Interfaces
{
    public interface IFAQRepository
    {
        Task<Animal> GetAnimalData(List<string> input);
    }
}
