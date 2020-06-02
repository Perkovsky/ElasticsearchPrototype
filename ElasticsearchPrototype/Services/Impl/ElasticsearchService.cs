using DAL;
using ElasticsearchPrototype.Models;
using Microsoft.EntityFrameworkCore;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticsearchPrototype.Services.Impl
{
	public class ElasticsearchService
	{
		private readonly string indexName;
		private readonly IElasticClient _client;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPrintService _printService;

		public ElasticsearchService(ElasticsearchSettings elasticsearchSettings, IUnitOfWork unitOfWork, IPrintService printService)
		{
			if (elasticsearchSettings == null)
				throw new ArgumentException("Value cannot be null or empty.", nameof(elasticsearchSettings));

			_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
			_printService = printService ?? throw new ArgumentNullException(nameof(printService));
			
			indexName = elasticsearchSettings.IndexName;

			var settings = new ConnectionSettings(new Uri(elasticsearchSettings.ConnectionString));
			settings.ThrowExceptions(alwaysThrow: true);
			settings.DisableDirectStreaming();
			settings.DefaultIndex(indexName);
			settings.PrettyJson(); // good for DEBUG
			_client = new ElasticClient(settings);
		}

		#region Private Methods

		private IEnumerable<string> GetStopWords()
		{
			return _unitOfWork.SoundexStopWords.Select(n => n.StopWord);
		}

		private IEnumerable<string> GetSynonyms()
		{
			return _unitOfWork.SoundexSynonyms.Select(n => $"{n.Value} => {n.Synonym}");
		}

		private async Task<bool> IsIndexExistsAsync()
		{
			var result = await _client.Indices.ExistsAsync(indexName);
			return result.Exists;
		}

		private async Task DeleteIndexAsync()
		{
			if (await IsIndexExistsAsync())
				await _client.Indices.DeleteAsync(indexName);
		}

		private async Task<CreateIndexResponse> CreateIndexAsync()
		{
			// also see:
			// https://www.elastic.co/guide/en/elasticsearch/client/net-api/7.x/writing-analyzers.html
			// https://www.elastic.co/guide/en/elasticsearch/client/net-api/7.x/testing-analyzers.html#_testing_a_custom_analyzer_in_an_index

			await DeleteIndexAsync();

			var result = await _client.Indices.CreateAsync(indexName, c => c
				.Settings(s => s
					.Analysis(a => a
						.TokenFilters(tf => tf
							.Stop("building_stop", sw => sw
								.StopWords(GetStopWords())
							)
							.Synonym("building_synonym", sf => sf
								.Synonyms(GetSynonyms())
							)
						)
						.Analyzers(an => an
							.Custom("index_building", ca => ca
								.CharFilters("html_strip")
								.Tokenizer("standard")
								.Filters("lowercase", "stop", "building_stop", "building_synonym")

							)
							.Custom("search_building", ca => ca
								.Tokenizer("standard")
								.Filters("lowercase", "stop", "building_stop", "building_synonym")
							)
						)
					)
				)
				.Map<BuildingSoundex>(mm => mm
					.AutoMap()
					.Properties(p => p
						.Text(t => t
							.Name(n => n.Address)
							.Analyzer("index_building")
							.SearchAnalyzer("search_building")
						)
					)
				)
			);

			if (!result.IsValid)
				_printService.PrintError(result.ServerError.Error.ToString());

			return result;
		}

		private async Task<IEnumerable<BuildingSoundex>> GetLoadData(int skipItems, int partSize)
		{
			return await _unitOfWork.Buildings
				.Select(n => new BuildingSoundex
				{
					Id = n.Id,
					Address = n.Address1
				})
				.Skip(skipItems)
				.Take(partSize)
				.ToListAsync();
		}

		private async Task SeedDataInPartsAsync(int partSize)
		{
			int total = 0;
			int errors = 0;
			int skipItems = 0;

			while (true)
			{
				var part = await GetLoadData(skipItems, partSize);
				int count = part.Count(); //(part as ICollection<BuildingSoundex>).Count;
				if (count == 0)
					break;

				total += count;
				skipItems += partSize;
				var result = await _client.IndexManyAsync(part);

				if (!result.IsValid)
				{
					errors += result.ItemsWithErrors.Count();
					foreach (var item in result.ItemsWithErrors)
						_printService.PrintError($"Failed to index document {item.Id}: {item.Error}");
				}
			}

			_printService.PrintInfo($"Finished loading seed data to Elasticsearch.{Environment.NewLine}Total: {total}, Error(s): {errors}.{Environment.NewLine}");
		}

		#endregion

		public async Task SeedDataAsync(int partSize = 10000)
		{
			_printService.PrintInfo("Started loading seed data to Elasticsearch...");
			await CreateIndexAsync();
			await SeedDataInPartsAsync(partSize);
		}

		public async Task<IEnumerable<BuildingSoundex>> SearchAsync(string search)
		{
			int size = 1000;
			var result = await _client.SearchAsync<BuildingSoundex>(s => s
				.Size(size)
				.Query(q => q
					.QueryString(qs => qs
						.Query(search)
					)
				)
			);
			return result.Documents;
		}
	}
}
