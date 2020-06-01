using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
	public class Settings : DbEntity
	{
		[Column("_key")]
		[StringLength(255)]
		public string Key { get; set; }

		[Column("_value")]
		public string Value { get; set; }

		[Column("_description")]
		[Required]
		public string Description { get; set; }

		public bool IsEditable { get; set; }
	}
}
