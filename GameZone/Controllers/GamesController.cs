using GameZone.Models;
using GameZone.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoryService _categoryService;
        private readonly IGameService gameService;

        public GamesController(ApplicationDbContext context,
            ICategoryService categoryService,
            IGameService gameService)
        {
            _context = context;
            _categoryService = categoryService;
            this.gameService = gameService;
        }

        public IActionResult Index()
        {
            var games = gameService.GetAll();
            return View(games);
        }
        public IActionResult Details(int id)
        {
            var game = gameService.GetById(id);

            if (game is null)
                return NotFound();

            return View(game);
        }


        public IActionResult Create()
        {
            CreateGameFormViewModel Viewmodel = new CreateGameFormViewModel()
            {
                categories = _categoryService.GetSelectListItems(),

                devices = _categoryService.GetDevicesSelectListItems()


            };
            return View(Viewmodel);
        }

        [HttpPost]
    
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.categories = _categoryService.GetSelectListItems();

                model.devices = _categoryService.GetDevicesSelectListItems();


                return View(model);
            }
            await gameService.create(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
