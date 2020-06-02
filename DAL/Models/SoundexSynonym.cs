using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
	public class SoundexSynonym : DbEntity
	{
		[StringLength(255)]
		public string Value { get; set; }

		/// <summary>
		/// Matching one-to-one. Value has only one synonym.
		/// </summary>
		[StringLength(255)]
		public string Synonym { get; set; }

		/// <summary>
		/// Use a comma to separate synonyms.
		/// </summary>
		/// <remarks>
		/// Example: "big,large,huge"
		/// </remarks>
		[Obsolete("Not working correctly. Use the 'Synonym' property.", true)]
		public string Synonyms { get; set; }
	}
}
