using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CTSolution.Models
{
    public class PurchaseImportMaster
    {
        
        public string TransactionID { get; set; } // "yyyy MM dd hh mm ss ms"

        [Key]
        public int PurchaseImportPkid { get; set;}

        [Required]
        public string PTK_FormNo { get; set; }

        [Display(Name = "တင်သွင်းသူအမည်")]
        [Required]
        public int ImporterPkid { get; set; }

        //[Display(Name = "တင်သွင်းသူအမည်")]
        [NotMapped]
       public string PersonName { get; set; }

        [Display(Name = "သွင်းကုန်ကြေညာလွှာအမှတ်")]
        public string DeclarationFormNo { get; set; }

        [Display(Name = "သွင်းကုန်ကြေညာလွှာရက်စွဲ")]
        public DateTime DeclarationFormDate { get; set; }

        
        public DateTime TransactionDate { get; set; }

        [Display(Name = "စည်းကြပ်မှုခံယူသည့်ရုံး")]
        public string TaxOffice { get; set; }

        [Display(Name = "အခွန်အရာရှိ")]
        public string TaxOfficer { get; set; }

        public bool IsClosed { get; set; } = false;

        [Display(Name = "မှတ်ချက်")]
        public string Note { get; set; }

        public ICollection<PurchaseImportDetail> PurchaseImportDetail { get; set; } = new List<PurchaseImportDetail>();

        public TaxPayerInfo TaxPayerInfo { get; set; }

        [Display(Name = "ကျသင့်အခွန်နှုန်း (ကျပ်)")]
        [NotMapped]
        public decimal TaxAmt { get; set; }

        [Display(Name = "အခွန်ထမ်းမှတ်ပုံတင်အမှတ် (TIN)")]
        [NotMapped]
        public string TIN_Number { get; set; }


        [Display(Name = "သွင်းကုန်လိုင်စင်အမှတ် ")]
        [NotMapped]
        public string CommercialLicense { get; set; }


        [Display(Name = "ပေးသွင်းအခွန်")]
        [NotMapped]
        public decimal? PaidAmt { get; set; }

        [Display(Name = "ကျန်ရှိအခွန်")]
        [NotMapped]
        public decimal? BalanceAmt { get; set; }
    }
}
