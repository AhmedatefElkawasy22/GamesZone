using GamesZone.Models;
using GamesZone.My_Settings;
using GamesZone.ViewModels;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;

namespace GamesZone.Services
{
	public class GameServices : IGameServices
	{
		private readonly Context _context;
		private readonly IWebHostEnvironment _environment;
		private readonly string _ImagePath;
		public GameServices(Context context, IWebHostEnvironment environment)
		{
			_context = context;
			_environment = environment;
			_ImagePath = $"{_environment.WebRootPath}{FileSettings.ImagesPath}";
		}


		public IEnumerable<Game> GetAll()
		{
			return _context.Game.AsNoTracking().ToList();
		}
		public IEnumerable<Game> GetAllIncludedCategoryAndDevices()
		{
			return _context.Game.Include(d => d.Category).Include(d => d.Devices).ThenInclude(g => g.device).AsNoTracking().ToList();
		}
		public Game? GetById(int id)
		{
			Game? game = _context.Game.AsNoTracking().FirstOrDefault(d => d.Id == id);
			if (game == null)
			{
				throw new KeyNotFoundException($"No game found with ID {id}.");
			}
			return game;
		}
		public Game? GetByIdIncludedCategoryAndDevices(int id)
		{
			Game? game = _context.Game.Include(d => d.Category).Include(d => d.Devices).ThenInclude(g => g.device).AsNoTracking().FirstOrDefault(d => d.Id == id);
			return game;
		}
		public async Task<int> Create(CreateGameViewModel game)
		{
			var coverName = await CreateImg(game.Cover);
			Game add = new()
			{
				UserId = game.UserId,
				Name = game.Name,
				Description = game.Description,
				Cover = coverName,
				CategoryID = game.CategoryID,
				Devices = game.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList(),
			};
			_context.Game.Add(add);
			return _context.SaveChanges();
		}

		public int Delete(int id, string userId, bool isAdmin)
		{
			Game? game = GetById(id);

			if (game == null)
				return 0;
			
			// Check if the user is an admin or the owner of the game
			if (!isAdmin && game.UserId != userId)
			{
				// User is not authorized to delete the game
				return 0;
			}

			_context.Game.Remove(game);
			int res = _context.SaveChanges();

			if (res > 0)
			{
				string cover = Path.Combine(_ImagePath, game.Cover);
				File.Delete(cover);
			}

			return res;
		}


		public async Task<int> Update(UpdateGameViewModel game)
		{
			Game? old = _context.Game.Include(d => d.Devices).FirstOrDefault(e => e.Id == game.id);
			if (old is null)
				return -1;
			//if (old.UserId != game.UserId)
			//	return -2;

			string oldCover = old.Cover;
			bool hasNewCover = game.Cover is not null;

			if (hasNewCover)
			{
				string coverName = await CreateImg(game.Cover);
				old.Cover = coverName;
			}
			old.UserId = game.UserId;
			old.Name = game.Name;
			old.Description = game.Description;
			old.CategoryID = game.CategoryID;

			old.Devices = game.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();

			_context.Game.Update(old);

			int res = await _context.SaveChangesAsync();
			if (res > 0)
			{
				if (hasNewCover)
				{
					string cover = Path.Combine(_ImagePath, oldCover);
					File.Delete(cover);
				}
			}
			else
			{
				string cover = Path.Combine(_ImagePath, old.Cover);
				File.Delete(cover);
			}
			return res;
		}

		private async Task<string> CreateImg(IFormFile cover)
		{
			var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
			var path = Path.Combine(_ImagePath, coverName);
			using var stream = File.Create(path);
			await cover.CopyToAsync(stream);
			return coverName;
		}
	}
}
