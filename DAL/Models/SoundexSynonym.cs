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
		[Obsolete("Use the 'Synonyms' property for more compact notation.", true)]
		[StringLength(255)]
		public string Synonym { get; set; }

		/// <summary>
		/// Use a comma to separate synonyms.
		/// </summary>
		/// <remarks>
		/// Example: "big,large,huge"
		/// </remarks>
		public string Synonyms { get; set; }
	}
}
