using GamesZone.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GamesZone.Services
{
    public class CategoriesService : ICategoriesAndDevicesService<CategoriesService>
    {
        private readonly Context _context;

        public CategoriesService(Context context) { _context = context; }
        public IEnumerable<SelectListItem> selectListItems()
        {
            return _context.Category.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).OrderBy(c => c.Text).ToList();
        }
    }
}
