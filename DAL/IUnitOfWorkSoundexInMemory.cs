using DAL.Models;
using System.Collections.Generic;

namespace DAL
{
	public interface IUnitOfWorkSoundexInMemory
	{
		IEnumerable<SoundexMapping> SoundexMappings { get; }

		IEnumerable<SoundexStopWord> SoundexStopWords { get; }
	}
}
