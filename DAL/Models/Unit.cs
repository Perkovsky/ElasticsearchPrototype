using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
	public class Unit : DbEntity
	{
		[StringLength(20)]
		public string Apartment { get; set; }

		public bool Status { get; set; }

		[StringLength(55)]
		public string TenantCode { get; set; }

		public int UnitType { get; set; }

		[Column(TypeName = "money")]
		public decimal? Balance { get; set; }

		public int? ExternalId { get; set; }

		public DateTime? CreatedDate { get; set; }

		public bool StopPayments { get; set; }

		[Column(nameof(Building))]
		public int BuildingId { get; set; }

		[ForeignKey(nameof(BuildingId))]
		public Building Building { get; set; }
	}
}
