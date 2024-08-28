using CTSolution.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CTSolution.Controllers
{
    public class PurchaseImportMasterController : Controller
    {
        private readonly ApplicationDbContextcs _context;

        public PurchaseImportMasterController(ApplicationDbContextcs context)
        {
            _context = context;
        }

        // GET: PurchaseImportMaster
       
        public IActionResult Index()
        {
            var model = from pim in _context.PurchaseImportMaster
                        join tpi in _context.TaxPayerInfo on pim.ImporterPkid equals tpi.TaxPayerPkid
                        select new PurchaseImportMaster
                        {
                            TransactionID = pim.TransactionID,
                            TransactionDate = pim.TransactionDate,
                            PurchaseImportPkid = pim.PurchaseImportPkid,
                            PersonName = tpi.PersonName,
                            DeclarationFormNo = pim.DeclarationFormNo,
                            TIN_Number = tpi.TIN_Number,
                            CommercialLicense = tpi.CommercialLicense,
                            DeclarationFormDate = pim.DeclarationFormDate,
                            TaxOffice = pim.TaxOffice,
                            TaxOfficer = pim.TaxOfficer,
                            Note = pim.Note,
                            TaxAmt = pim.TaxAmt,
                        };

            var purchaseImportMasters = model.ToList();
            foreach ( var pur in  purchaseImportMasters )
            {
                Console.WriteLine( pur.TransactionID );
               var transaction =  _context.PurchaseTransaction.Where(P => P.TransactionID == pur.TransactionID ).FirstOrDefault();
                pur.PaidAmt = transaction?.PaidAmt;
                pur.BalanceAmt = transaction?.BalanceAmt;

                var detail = _context.PurchaseImportDetail.Where(P => P.TransactionID == pur.TransactionID).FirstOrDefault();
                pur.TaxAmt = detail.TaxAmt;
            }
            return View(purchaseImportMasters);
        }





        // GET: PurchaseImportMaster/Create
        public IActionResult Create()
        {
            var importers = _context.TaxPayerInfo
                .Where(t => t.BusinessType == "Importer")
                .Select(t => new
                {
                    t.TaxPayerPkid,
                    t.PersonName
                })
                .ToList();

            var model = new PurchaseImportMaster
            {
                
            };

            // Add a default item at the beginning of the list
            var selectList = new List<SelectListItem>
    {
        new SelectListItem
        {
            Value = "",
            Text = "Select Tax Payer Name" 
        }
    };

            selectList.AddRange(importers.Select(i => new SelectListItem
            {
                Value = i.TaxPayerPkid.ToString(),
                Text = i.PersonName
            }));

            ViewBag.Importers = selectList;

            
            return View(model);
        }




        // POST: PurchaseImportMaster/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseImportMaster purchaseImportMaster)
        {
            try
            {
                // Set the TransactionID here
                purchaseImportMaster.TransactionID = DateTime.Now.ToString("yyyyMMddHHmmssfff");

                // Set the TransactionDate here
                purchaseImportMaster.TransactionDate = DateTime.Now;

                _context.Add(purchaseImportMaster);

                await _context.SaveChangesAsync();

                return RedirectToAction("Create", "PurchaseImportDetail", new { transactionId = purchaseImportMaster.TransactionID });
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Database update error: {dbEx.Message}");
                ModelState.AddModelError("", "An error occurred while saving data. Please try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
            }

            // Reload importers if there was an error
            var importers = _context.TaxPayerInfo
                .Where(t => t.BusinessType == "Importer")
                .Select(t => new SelectListItem
                {
                    Value = t.TaxPayerPkid.ToString(),
                    Text = t.PersonName
                })
                .ToList();

            ViewBag.Importers = importers;
            return View(purchaseImportMaster);
        }


        // GET: PurchaseImportMaster/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var purchaseImportMaster = await _context.PurchaseImportMaster.FindAsync(id);
            Console.WriteLine("here id...................." + id);
            Console.WriteLine("here id...................." + (purchaseImportMaster == null));
            Console.WriteLine("here id...................." + (purchaseImportMaster.TransactionID));
            
            if (purchaseImportMaster == null)
            {
                return NotFound();
            }

            ViewBag.Importers = new SelectList(
             _context.TaxPayerInfo
                 .Where(t => t.BusinessType == "Importer")
                 .Select(t => new { t.TaxPayerPkid, t.PersonName })
                 .ToList(),
             "TaxPayerPkid", 
             "PersonName"   
         );
            return View(purchaseImportMaster);
        }

        // POST: PurchaseImportMaster/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PurchaseImportMaster purchaseImportMaster)
        {
            if (id != purchaseImportMaster.PurchaseImportPkid)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseImportMaster);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseImportMasterExists(purchaseImportMaster.PurchaseImportPkid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewBag.Importers = new SelectList(
                _context.TaxPayerInfo.Where(t => t.BusinessType == "Importer"),
                "TaxPayerPkid",
                "PersonName", // Display the ID or modify as needed
                purchaseImportMaster.ImporterPkid
            );

            return View(purchaseImportMaster);
        }

        // GET: PurchaseImportMaster/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var purchaseImportMaster = await _context.PurchaseImportMaster
                .FirstOrDefaultAsync(m => m.PurchaseImportPkid == id);
            if (purchaseImportMaster == null)
            {
                return NotFound();
            }

            return View(purchaseImportMaster);
        }

        // POST: PurchaseImportMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseImportMaster = await _context.PurchaseImportMaster.FindAsync(id);
            if (purchaseImportMaster != null)
            {
                _context.PurchaseImportMaster.Remove(purchaseImportMaster);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        // GET: PurchaseImportMaster/Detail/5
        public async Task<IActionResult> Details(int id)
        {
            var purchaseImportMaster = await _context.PurchaseImportMaster
                .Include(p => p.PurchaseImportDetail)
                .Include(p => p.TaxPayerInfo)
                .FirstOrDefaultAsync(p => p.PurchaseImportPkid == id);

            if (purchaseImportMaster == null)
            {
                return NotFound();
            }

            // Debugging: Check the count of PurchaseImportDetail
            Console.WriteLine($"Details count: {purchaseImportMaster.PurchaseImportDetail.Count}");

            return View(purchaseImportMaster);
        }




        private bool PurchaseImportMasterExists(int id)
        {
            return _context.PurchaseImportMaster.Any(e => e.PurchaseImportPkid == id);
        }

        [HttpGet]
        public JsonResult GetTinComByTaxPayerPkid(int id)
        {
            TaxPayerInfo tp =  _context.TaxPayerInfo.Where(p => p.TaxPayerPkid == id).FirstOrDefault();
            return(Json(tp));
        }
    }
}
