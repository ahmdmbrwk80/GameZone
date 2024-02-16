using GameZone.Models;
using Microsoft.AspNetCore.Hosting;

namespace GameZone.Services
{
    public class GameService : IGameService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        private readonly string _imagepath;

        public GameService(ApplicationDbContext context, IWebHostEnvironment WebHostEnvironment)
        {
            _context = context;
            _WebHostEnvironment = WebHostEnvironment;
            _imagepath = $"{_WebHostEnvironment.WebRootPath}/assets/images/games";
        }


        public async Task create(CreateGameFormViewModel model)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";
            var path = Path.Combine(_imagepath, coverName);

            using var stream = File.Create(path);

            await model.Cover.CopyToAsync(stream);
            stream.Dispose();

            Game game = new Game()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover = coverName,
                Devices = model.selectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()

            };

            _context.Add(game);
            _context.SaveChanges();
        }
        public Game? GetById(int id)
        {
            return _context.Games
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .SingleOrDefault(g => g.Id == id);
        }
        public IEnumerable<Game> GetAll()
        {
            return _context.Games
                .Include(c => c.Category)
                .Include(c => c.Devices)
                .ThenInclude(d=>d.Device       )
                .AsNoTracking().ToList();
        }
    }
}
