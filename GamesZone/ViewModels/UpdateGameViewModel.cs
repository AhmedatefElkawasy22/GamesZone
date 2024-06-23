using GamesZone.Attributes;
using GamesZone.My_Settings;
using System.ComponentModel.DataAnnotations;

namespace GamesZone.ViewModels
{
    public class UpdateGameViewModel : FormViewModel
    {
        public int id { get; set; }
        public string? CoverPath { get; set; }

        [DataType(DataType.Upload)]
        [AllowedExtensions(FileSettings.AllowedExtensions)]
        [MaxFileSize(FileSettings.MaxSizeFileInBytes)]
        public IFormFile? Cover { get; set; }
    }
}
