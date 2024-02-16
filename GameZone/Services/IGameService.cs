using GameZone.Models;

namespace GameZone.Services
{
    public interface IGameService
    {
        Task create (CreateGameFormViewModel model);
        Game? GetById(int id);
        IEnumerable<Game> GetAll();
    }
}
