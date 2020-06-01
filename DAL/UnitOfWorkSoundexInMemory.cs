using DAL.Models;
using System.Collections.Generic;

namespace DAL
{
	public class UnitOfWorkSoundexInMemory : IUnitOfWorkSoundexInMemory
	{
		public IEnumerable<SoundexMapping> SoundexMappings => new List<SoundexMapping>
		{
			new SoundexMapping { Text = "1", MatchText = "first" },
			new SoundexMapping { Text = "1st", MatchText = "first" },
			new SoundexMapping { Text = "1-st", MatchText = "first" },
			new SoundexMapping { Text = "one", MatchText = "first" },
			new SoundexMapping { Text = "One", MatchText = "first" },
		};

		public IEnumerable<SoundexStopWord> SoundexStopWords => new List<SoundexStopWord>
		{
			new SoundexStopWord { StopWord = "avenue" },
			new SoundexStopWord { StopWord = "ave" },
		};
	}
}
