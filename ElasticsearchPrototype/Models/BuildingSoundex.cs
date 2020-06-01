using Nest;

namespace ElasticsearchPrototype.Models
{
	public class BuildingSoundex
	{
		[Number(Index = false)] // property excluded from search
		public int Id { get; set; }

		public string Address { get; set; }
	}
}
