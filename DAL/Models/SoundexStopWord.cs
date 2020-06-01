using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
	public class SoundexStopWord : DbEntity
	{
		[StringLength(255)]
		public string StopWord { get; set; }
	}
}
