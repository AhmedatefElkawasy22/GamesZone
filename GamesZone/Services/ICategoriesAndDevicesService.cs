using Microsoft.AspNetCore.Mvc.Rendering;

namespace GamesZone.Services
{
    public interface ICategoriesAndDevicesService <T>
    {
        IEnumerable<SelectListItem> selectListItems();
    }
}
