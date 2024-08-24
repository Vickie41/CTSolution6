using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CTSolution.Models
{
    public class PurchaseImportDetail
    {
        [Key]
        public int PurchaseImportDetailPkid { get; set; }

        
        public string TransactionID { get; set; }

        [Display(Name = "ရောင်းချသည့် ကုန်ပစ္စည်း / ဝန်ဆောင်မှုအမည်")]
        public string GoodsServicesName { get; set; }

        [Display(Name = "ရေတွက်ပုံ")]

        public string UnitType { get; set; }

        [Display(Name = "အရေအတွက်")]
        public int Qty { get; set; }

        [Display(Name = "သွင်းကုန်တန်ဖိုး (နိူင်ငံခြားငွေ)")]
        public string ImportValue { get; set; }

        [Display(Name = "ကုန်းပေါ်ရောက်တန်ဖိုး (ကျပ်)")]
        public string ValueOnLand { get; set; }

        [Display(Name = "အခွန်နှုန်းထား")]
        public decimal TaxRate { get; set; }

        [Display(Name = "ကျသင့်အခွန်နှုန်း (ကျပ်)")]
        public decimal TaxAmt { get; set; }

        [Display(Name = "မှတ်ချက်")]
        public string Note { get; set; }

        [NotMapped]
        public string PersonName { get; set; }
        public virtual PurchaseImportMaster PurchaseImportMaster { get; set; }
    }
}
