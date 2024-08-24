using System.ComponentModel.DataAnnotations;

namespace CTSolution.Models
{
    public class TaxPayerInfo
    {
        [Key]
        public int TaxPayerPkid { get; set; }

        [Display(Name = "အခွန်ထမ်းအမည်")]
        public string PersonName { get; set; }

        [Display(Name = "မှတ်ပုံတင်အမှတ်")]
        public string NRCNo { get; set; }

        [Display(Name = "လုပ်ငန်း / ဆိုင် အမည်")]
        public string BusinessName { get; set; }

        [Display(Name = "ကုမ္မဏီအမည်")]
        public string CompanyName { get; set; }
        
        [Display(Name = "လုပ်ငန်းမှတ်ပုံတင်အမှတ်")]
        public string BizRegNumber { get; set; }

        [Display(Name = "အခွန်ထမ်းမှတ်ပုံတင်အမှတ် (TIN)")]
        public string TIN_Number { get; set; }

        [Display(Name = "လုပ်ငန်း အမျိူးအစား")]
        public string BusinessType { get; set; }

        [Display(Name = "သွင်းကုန်လိုင်စင်အမှတ် ")]
        public string CommercialLicense { get; set; }

        [Display(Name = "သွင်းကုန်လိုင်စင်ရက်စွဲ ")]
        public DateTime CommercialLicenseDate { get; set; }

        [Display(Name = "ဖုန်းနံပါတ် ")]
        public string PhoneNo { get; set; }

        [Display(Name = "အီးမေးလ် ")]
        public string Email { get; set; }

        [Display(Name = "နေရပ်လိပ်စာ ")]
        public string Address { get; set; }

        [Display(Name = "မြို့နယ် ")]
        public string Township { get; set; }


        public static List<string> BusinessTypes => new List<string>
        {
            "Importer",
            "Exporter",
            "Manufacturer",
            "Wholesaler",
            "Retailer"
         };

        public string TransactionID { get; internal set; }
        public DateTime TransactionDate { get; internal set; }
        public string TaxOffice { get; internal set; }
        public string TaxOfficer { get; internal set; }
    }
}
