using ElasticsearchPrototype.Models;
using System;
using System.Collections.Generic;
using System.Web;

namespace ElasticsearchPrototype.Services.Impl
{
	public class PrintService : IPrintService
	{
		public void PrintInfo(string text, bool useTimestamp = true)
		{
			if (useTimestamp)
				Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fffff}] - {text}");
			else
				Console.WriteLine(text);
		}

		public void PrintInfo(IEnumerable<Building> items)
		{
			Console.WriteLine("Item(s):");
			foreach (var item in items)
				Console.WriteLine($"\tID: {item.Id} \tAddress: {HttpUtility.HtmlDecode(item.Address)}");
		}

		public void PrintError(string text)
		{
			var defaultTextColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(text);
			Console.ForegroundColor = defaultTextColor;
		}
	}
}
