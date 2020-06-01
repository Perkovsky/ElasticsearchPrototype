using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
	public class Company : DbEntity
	{
		[Required(AllowEmptyStrings = true)]
		[StringLength(255)]
		public string Code { get; set; }

		[Required(AllowEmptyStrings = true)]
		[StringLength(255)]
		public string Name { get; set; }

		[StringLength(255)]
		public string ContactPerson { get; set; }

		[StringLength(30)]
		public string Phone1 { get; set; }

		[StringLength(30)]
		public string Phone2 { get; set; }

		[StringLength(30)]
		public string Fax { get; set; }

		public bool Status { get; set; }

		[Column(TypeName = "MONEY")]
		public decimal TransactionPrice { get; set; }

		[Column(TypeName = "VARCHAR(MAX)")]
		public string Description { get; set; }

		[Column(TypeName = "VARCHAR(255)")]
		[StringLength(255)]
		public string Email { get; set; }

		[Column(TypeName = "MONEY")]
		public decimal PmcFee { get; set; }

		[Column(TypeName = "MONEY")]
		public decimal MaximumAmount { get; set; }

		public bool IsUseSoundex { get; set; }

		[Column(TypeName = "MONEY")]
		public decimal MinimumAmount { get; set; }

		public decimal? PmcSubscriptionFee { get; set; }

		[Column("LastSubscription")]
		public DateTime? NextSubscriptionDate { get; set; }

		public DateTime CreateDate { get; set; }

		public DateTime ModifyDate { get; set; }

		public bool DirectTenantPortal { get; set; }

		public bool EnableRemoteLandlord { get; set; }

		[StringLength(55)]
		public string RemoteLandlordUsername { get; set; }

		[StringLength(55)]
		public string RemoteLandlordPassword { get; set; }

		public DateTime? RemoteLandlordLastProcessedDateTime { get; set; }

		[Column("RentManagerEnabled")]
		public bool EnableRentManagerd { get; set; }

		public DateTime? RentManagerLastProcessedDateTime { get; set; }

		public bool EnabledSubscription { get; set; }

		[Column("AutoApprove")]
		public bool UseTenantAutoApprove { get; set; }

		public bool EnableLeasehold { get; set; }

		public DateTime? LeaseholdLastProcessedDateTime { get; set; }

		public bool EnableYardi { get; set; }

		public DateTime? YardiLastProcessedDateTime { get; set; }

		[Column("EnableTov")]
		public bool EnableTovRent { get; set; }

		[Column("TovLastProcessedDateTime")]
		public DateTime? TovRentLastProcessedDateTime { get; set; }

		[StringLength(55)]
		public string TovRentUsername { get; set; }

		[StringLength(55)]
		public string TovRentPassword { get; set; }

		public bool MaintenanceDisabled { get; set; }

		public string XmlData { get; set; }

		public bool EnableMvc { get; set; }

		public bool AutoCloseComment { get; set; }

		public string AutoCloseCommentText { get; set; }

		[StringLength(255)]
		public string CustomMaintenanceTabName { get; set; }

		public bool EventEmailNotificationEnabled { get; set; }

		public bool LeaseDateRequired { get; set; }

		public bool RentalApplicationEnabled { get; set; }

		public string YardiSettings { get; set; }

		public bool DatawireEnabled { get; set; }

		public bool EnableMds { get; set; }

		public DateTime? MdsLastProcessedDateTime { get; set; }

		[StringLength(255)]
		public string MdsClientCode { get; set; }

		[StringLength(5)]
		public string MdsCompanyName { get; set; }

		[StringLength(55)]
		public string RentManagerUsername { get; set; }

		[StringLength(55)]
		public string RentManagerPassword { get; set; }

		public bool EnableRentManagerImport { get; set; }

		public bool EnableAcademy { get; set; }

		public DateTime? AcademyStartDate { get; set; }

		[StringLength(55)]
		public string AcademyUsername { get; set; }

		[StringLength(55)]
		public string AcademyPassword { get; set; }

		public bool RentManagerSeperateFiles { get; set; }

		public bool EnableMdsUpdate { get; set; }

		public DateTime? MdsLastUpdateDate { get; set; }

		public bool EnableRmHistory { get; set; }

		public DateTime? RmHistoryLastUpdateDate { get; set; }

		public bool EnableMdsBalance { get; set; }

		public bool EnableWeimark { get; set; }

		[StringLength(55)]
		public string WeimarkUsername { get; set; }

		[StringLength(55)]
		public string WeimarkPassword { get; set; }

		[StringLength(55)]
		public string RemoteLandlordApiClientName { get; set; }

		public bool RentalApplicationIntegrationDisabled { get; set; }

		public bool? PcBankEnabled { get; set; }

		public bool? BitcoinEnabled { get; set; }

		public bool EnableSkyline { get; set; }

		public DateTime? SkylineStartDate { get; set; }

		[StringLength(16)]
		public string BankCompanyName { get; set; }

		[StringLength(10)]
		public string BankCompanyDescription { get; set; }

		public bool EnableNewDesign { get; set; }
		public bool AccessGuarantedEnabled { get; set; }

		public bool EnableAmenity { get; set; }
	}
}