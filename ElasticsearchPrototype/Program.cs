using ElasticsearchPrototype.Models;
using ElasticsearchPrototype.Services.Impl;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticsearchPrototype
{
	class Program
	{
		static async Task Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;

			IConfiguration config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();
			var elasticsearchSettings = new ElasticsearchSettings();
			config.Bind(nameof(ElasticsearchSettings), elasticsearchSettings);

			var unitOfWork = new UnitOfWork();
			var printService = new PrintService();
			var elasticsearchService = new ElasticsearchService(elasticsearchSettings, unitOfWork, printService);
			await elasticsearchService.SeedDataAsync();

			printService.PrintInfo($"Enter sesrch text (or quit to exit)...{Environment.NewLine}", false);
			
			// input search text: "1", "1st", "first", "one", "One", "ONe", "First", "FIRST", "FiRsT", "2nd", "avenue", "Avenue", "1 ave", "1 avenue", "ave"
			while (true)
			{
				Console.Write("> ");
				string search = Console.ReadLine();
				if (search.Equals("quit", StringComparison.InvariantCultureIgnoreCase))
					break;

				var result = await elasticsearchService.SearchAsync(search);
				printService.PrintInfo(result);
				printService.PrintInfo($"Total items founded by search='{search}': {result.Count()}{Environment.NewLine}", false);
			}
		}
	}
}
