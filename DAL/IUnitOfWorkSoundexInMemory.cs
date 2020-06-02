using DAL.Models;
using System.Collections.Generic;

namespace DAL
{
	public interface IUnitOfWorkSoundexInMemory
	{
		IEnumerable<SoundexSynonym> SoundexSynonyms { get; }

		IEnumerable<SoundexStopWord> SoundexStopWords { get; }
	}
}
