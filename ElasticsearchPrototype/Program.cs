using DAL;
using ElasticsearchPrototype.Models;
using ElasticsearchPrototype.Services.Impl;
using Microsoft.EntityFrameworkCore;
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
			var printService = new PrintService();

			IConfiguration config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();
			var elasticsearchSettings = new ElasticsearchSettings();
			config.Bind(nameof(ElasticsearchSettings), elasticsearchSettings);

			var connectionString = config.GetConnectionString("mainDb");
			var optionsBuilder = new DbContextOptionsBuilder<UnitOfWork>();
			var options = optionsBuilder.UseSqlServer(connectionString).Options;
			var unitOfWork = new UnitOfWork(options);

			var elasticsearchService = new ElasticsearchService(elasticsearchSettings, unitOfWork, printService);
			await elasticsearchService.SeedDataAsync();

			printService.PrintInfo($"Enter search text (or quit to exit)...{Environment.NewLine}", false);
			
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
