using ElasticsearchPrototype.Models;
using System.Collections.Generic;

namespace ElasticsearchPrototype.Services
{
	public interface IUnitOfWork
	{
		IEnumerable<SoundexMapping> SoundexMappings { get; }

		IEnumerable<SoundexStopWord> SoundexStopWords { get; }
	}
}
