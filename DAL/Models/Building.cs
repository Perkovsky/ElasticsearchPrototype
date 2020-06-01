using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
	public class Building : DbEntity
	{
		[Required(AllowEmptyStrings = true)]
		[StringLength(30)]
		public string Name { get; set; }

		[Required(AllowEmptyStrings = true)]
		[StringLength(55)]
		public string ShortName { get; set; }

		[Required(AllowEmptyStrings = true)]
		[StringLength(55)]
		public string PropertyCode { get; set; }

		public bool Status { get; set; }

		public bool MaintenanceDisabled { get; set; }

		public int? ExternalId { get; set; }

		public int? DefaultBankId { get; set; }

		public DateTime? CreatedDate { get; set; }

		[Column(nameof(Company))]
		public int CompanyId { get;set; }

		[ForeignKey(nameof(CompanyId))]
		public Company Company { get; set; }

		[StringLength(255)]
		public string Address1 { get; set; }

		[StringLength(255)]
		public string Address2 { get; set; }

		[StringLength(255)]
		public string City { get; set; }

		[StringLength(255)]
		public string State { get; set; }

		[StringLength(10)]
		public string Zip { get; set; }
	}
}
