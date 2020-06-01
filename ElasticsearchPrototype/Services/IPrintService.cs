using ElasticsearchPrototype.Models;
using System.Collections.Generic;

namespace ElasticsearchPrototype.Services
{
	public interface IPrintService
	{
		void PrintInfo(IEnumerable<Building> items);
		void PrintInfo(string text, bool useTimestamp = true);
		void PrintError(string text);
	}
}
