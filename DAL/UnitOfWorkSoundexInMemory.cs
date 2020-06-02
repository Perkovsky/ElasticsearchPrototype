using DAL.Models;
using System.Collections.Generic;

namespace DAL
{
	public class UnitOfWorkSoundexInMemory : IUnitOfWorkSoundexInMemory
	{
		public IEnumerable<SoundexSynonym> SoundexSynonyms => new List<SoundexSynonym>
		{
			//new SoundexSynonym { Value = "1", Synonyms = "1st,1-st,1th,1-th,first,one" },
			new SoundexSynonym { Value = "1", Synonym = "first" },
			new SoundexSynonym { Value = "1st", Synonym = "first" },
			new SoundexSynonym { Value = "1-st", Synonym = "first" },
			new SoundexSynonym { Value = "1th", Synonym = "first" },
			new SoundexSynonym { Value = "1-th", Synonym = "first" },
			new SoundexSynonym { Value = "one", Synonym = "first" },
		};

		public IEnumerable<SoundexStopWord> SoundexStopWords => new List<SoundexStopWord>
		{
			new SoundexStopWord { StopWord = "avenue" },
			new SoundexStopWord { StopWord = "ave" },
		};
	}
}
