using GamesZone.Models;
using GamesZone.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GamesZone.Services
{
	public interface IGameServices
	{
		IEnumerable<Game> GetAll();
		IEnumerable<Game> GetAllIncludedCategoryAndDevices();
		Game? GetById(int id);
		Game? GetByIdIncludedCategoryAndDevices(int id);
		Task<int> Create(CreateGameViewModel game);
		int Delete(int id, string userId, bool isAdmin);
		Task<int> Update(UpdateGameViewModel game);
	}
}
