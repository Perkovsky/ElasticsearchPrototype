using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
	public class SoundexMapping : DbEntity
	{
		[StringLength(255)]
		public string Text { get; set; }

		[StringLength(255)]
		public string MatchText { get; set; }
	}
}
