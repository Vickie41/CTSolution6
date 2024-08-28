using System.ComponentModel.DataAnnotations;

namespace CTSolution.Models
{
    public class PurchaseTransaction
    {
        [Key]
        public int PurTransactionPkid { get; set; }

       
        public string? TransactionID { get; set; } 

        public DateTime? TransactionDate { get; set; }

        [Display(Name = "ချလန်အမှတ်")]
        public string? ChalenNo { get; set; }

        [Display(Name = "‌ကျသင့်အခွန်")]
        public decimal? TaxAmt { get; set; }

        [Display(Name = "ပေးသွင်းအခွန်")]
        public decimal? PaidAmt { get; set; }

        [Display(Name = "ကျန်ရှိအခွန်")]
        public decimal? BalanceAmt { get; set; }

        public bool? IsCompleted { get; set; }

        [Display(Name = "စည်းကြပ်မှုခံယူသည့်ရုံး")]
        public string? TaxOffice { get; set; }

        [Display(Name = "အခွန်အရာရှိ")]
        public string? TaxOfficer { get; set; }

        [Display(Name = "မှတ်ချက်")]
        public string? Note { get; set; }

        public bool? IsDeleted { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }
    }

}
