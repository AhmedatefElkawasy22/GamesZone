using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesZone.Models
{
	public class Game : BaseEntity
	{
		
		[MaxLength(2500)]
		public string Description { get; set; }
		[MaxLength(500)]
		public string Cover { get; set; }
		[ForeignKey("Category")]
		public int CategoryID { get; set; }
		public Category Category { get; set; }

		public List<GameDevice> Devices { get; set; }

	}
}
