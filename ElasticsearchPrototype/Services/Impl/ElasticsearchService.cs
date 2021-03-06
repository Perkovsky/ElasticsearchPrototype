﻿using ElasticsearchPrototype.Models;
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

		private string[] GetStopWords()
		{
			return _unitOfWork.SoundexStopWords.Select(n => n.StopWord).ToArray();
		}

		private string[] GetMappings()
		{
			return _unitOfWork.SoundexMappings.Select(n => $"{n.Text} => {n.MatchText}").ToArray();
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
			await DeleteIndexAsync();

			var result = await _client.Indices.CreateAsync(indexName, c => c
				.Settings(s => s
					.Analysis(a => a
						.TokenFilters(tf => tf
							.Stop("building_stop", sw => sw
								.StopWords(GetStopWords())
							)
						)
						.CharFilters(cf => cf
							.Mapping("building", mca => mca
								.Mappings(GetMappings())
							)
						)
						.Analyzers(an => an
							.Custom("index_building", ca => ca
								.CharFilters("html_strip", "building")
								.Tokenizer("standard")
								.Filters("lowercase", "stop", "building_stop")

							)
							.Custom("search_building", ca => ca
								.CharFilters("building")
								.Tokenizer("standard")
								.Filters("lowercase", "stop", "building_stop")
							)
						)
					)
				)
				.Map<Building>(mm => mm
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

		#endregion

		public async Task SeedDataAsync()
		{
			_printService.PrintInfo("Started loading seed data to Elasticsearch...");
			await CreateIndexAsync();
			var loadData = new List<Building>
			{
				new Building { Id = 777771, Address = "1 avenue"},
				new Building { Id = 777772, Address = "1st avenue"},
				new Building { Id = 777773, Address = "1-st avenue"},
				new Building { Id = 777774, Address = "First avenue"},
				new Building { Id = 777775, Address = "first avenue"},
				new Building { Id = 777776, Address = "One avenue"},
				new Building { Id = 777777, Address = "one avenue"},
				new Building { Id = 777778, Address = "2 avenue"},
				new Building { Id = 777779, Address = "2nd avenue"},
				new Building { Id = 777780, Address = "2-nd avenue"},
				new Building { Id = 777781, Address = "Second avenue"},
				new Building { Id = 777782, Address = "second avenue"},
				new Building { Id = 777783, Address = "Two avenue"},
				new Building { Id = 777784, Address = "two avenue"}
			};

			var result = await _client.IndexManyAsync(loadData);
			if (!result.IsValid)
			{
				foreach (var item in result.ItemsWithErrors)
					_printService.PrintError($"Failed to index document {item.Id}: {item.Error}");
			}
			_printService.PrintInfo("Finished loading seed data to Elasticsearch.");
		}

		public async Task<IEnumerable<Building>> SearchAsync(string search)
		{
			int size = 1000;
			var result = await _client.SearchAsync<Building>(s => s
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
