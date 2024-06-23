using GamesZone.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GamesZone.Services
{
    public class DevicesServise : ICategoriesAndDevicesService<DevicesServise>
    {
        private readonly Context _context;

        public DevicesServise(Context context) { _context = context; }
        public IEnumerable<SelectListItem> selectListItems()
        {
            return _context.Device.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).OrderBy(c => c.Text).ToList();
        }
    }
    
}
