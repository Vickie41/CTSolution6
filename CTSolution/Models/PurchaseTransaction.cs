using System.ComponentModel.DataAnnotations;

namespace CTSolution.Models
{
    public class PurchaseTransaction
    {
        [Key]
        public int PurTransactionPkid { get; set; }
        public string? TransactionID { get; set; } 

        public DateTime? TransactionDate { get; set; } 

        public string? ChalenNo { get; set; }

        public decimal? TaxAmt { get; set; } 

        public decimal? PaidAmt { get; set; }

        public decimal? BalanceAmt { get; set; }

        public bool? IsCompleted { get; set; } 

        public string? TaxOffice { get; set; } 

        public string? TaxOfficer { get; set; } 

        public string? Note { get; set; }

        public bool? IsDeleted { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }
    }

}
