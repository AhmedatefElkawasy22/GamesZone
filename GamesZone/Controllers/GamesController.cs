using GamesZone.Models;
using GamesZone.Services;
using GamesZone.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GamesZone.Controllers
{
	[Authorize]
	public class GamesController : Controller
	{
		private readonly ICategoriesAndDevicesService<CategoriesService> _categoriesService;
		private readonly ICategoriesAndDevicesService<DevicesServise> _devicesServise;
		private readonly IGameServices _gameServices;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public GamesController(ICategoriesAndDevicesService<CategoriesService> categoriesService,
							   ICategoriesAndDevicesService<DevicesServise> devicesServise,
							   IGameServices gameServices,
							   IHttpContextAccessor httpContextAccessor
							   )
		{
			_categoriesService = categoriesService;
			_devicesServise = devicesServise;
			_gameServices = gameServices;
			_httpContextAccessor = httpContextAccessor;
		}

		public IActionResult Index()
		{
			IEnumerable<Game> games = _gameServices.GetAllIncludedCategoryAndDevices();
			return View(games);
		}
		public IActionResult Details([FromRoute] int id)
		{
			Game? game = _gameServices.GetByIdIncludedCategoryAndDevices(id);
			if (game is null)
				return NotFound();

			return View(game);
		}
		[HttpGet]
		public IActionResult Create()
		{
			CreateGameViewModel model = new()
			{
				Categories = _categoriesService.selectListItems(),
				Devices = _devicesServise.selectListItems()
			};

			return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateGameViewModel data)
		{
			if (!ModelState.IsValid)
			{
				data.Categories = _categoriesService.selectListItems();
				data.Devices = _devicesServise.selectListItems();
				return View(data);
			}
			await _gameServices.Create(data);
			return RedirectToAction(nameof(Index));
		}
		public IActionResult Delete([FromRoute] int id)
		{
			// Get the current user's ID
			string currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			// Check if the current user is an admin
			bool isAdmin = _httpContextAccessor.HttpContext.User.IsInRole("Admin");

			int res = _gameServices.Delete(id, currentUserId, isAdmin);
			if (res == 0)
				return NotFound();
			return RedirectToAction("Index");
		}
		public IActionResult Update([FromRoute] int id)
		{
			Game? game = _gameServices.GetByIdIncludedCategoryAndDevices(id);

			if (game is null)
				return NotFound();

			UpdateGameViewModel model = new()
			{
				UserId = game.UserId,
				id = id,
				Name = game.Name,
				CategoryID = game.CategoryID,
				SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
				Description = game.Description,
				Categories = _categoriesService.selectListItems(),
				Devices = _devicesServise.selectListItems(),
				CoverPath = game.Cover,
			};

			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> Update([FromRoute] int id, UpdateGameViewModel game)
		{
			if (!ModelState.IsValid)
			{
				game.Categories = _categoriesService.selectListItems();
				game.Devices = _devicesServise.selectListItems();

				foreach (var key in ModelState.Keys)
				{
					var errors = ModelState[key].Errors;
					foreach (var error in errors)
					{
						ModelState.AddModelError("", error.ErrorMessage);
					}
				}
				return View(game);
			}
			int res = await _gameServices.Update(game);
			if (res == -1) { return NotFound(); }
			return RedirectToAction(nameof(Details), new { id = id });
		}
	}
}
