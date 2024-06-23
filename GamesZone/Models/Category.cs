namespace GamesZone.Models
{
	public class Category :BaseEntity
	{
		public List<Game> Games { get; set; }
	}
}
