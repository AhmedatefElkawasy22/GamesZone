using GamesZone.Attributes;
using GamesZone.My_Settings;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GamesZone.ViewModels
{
	public class CreateGameViewModel : FormViewModel
	{
		
        [Required]
        [DataType(DataType.Upload)]
		[AllowedExtensions(FileSettings.AllowedExtensions)]
        [MaxFileSize(FileSettings.MaxSizeFileInBytes)]
        public IFormFile Cover { get; set; } = default!;
	}
}
