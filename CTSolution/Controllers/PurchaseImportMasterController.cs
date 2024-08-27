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
            Console.WriteLine("here index...............................");
            var model = from pim in _context.PurchaseImportMaster
                        join tpi in _context.TaxPayerInfo on pim.ImporterPkid equals tpi.TaxPayerPkid
                        select new PurchaseImportMaster
                        {
                            TransactionID = pim.TransactionID,
                            TransactionDate = pim.TransactionDate,
                            PurchaseImportPkid = pim.PurchaseImportPkid,
                            PersonName = tpi.PersonName,
                            DeclarationFormNo = pim.DeclarationFormNo,
                            DeclarationFormDate = pim.DeclarationFormDate,
                            TaxOffice = pim.TaxOffice,
                            TaxOfficer = pim.TaxOfficer,
                            Note = pim.Note
                        };
            Console.WriteLine("after model....................");
            Console.WriteLine("count..................." +  model.ToList().Count);
            Console.WriteLine("before loop....................");
            List<PurchaseImportMaster> purchaseImportMasters = model.ToList();
            foreach (PurchaseImportMaster purchaseImportMaster in purchaseImportMasters)
            {
                Console.WriteLine("trans id......................." + purchaseImportMaster.TransactionID);
                PurchaseImportDetail purchaseImportDetail = _context.PurchaseImportDetail.FirstOrDefault(p => p.TransactionID == purchaseImportMaster.TransactionID);
                if(purchaseImportDetail != null)
                {
                    purchaseImportMaster.TaxAmt = purchaseImportDetail.TaxAmt;
                }
                //purchaseImportMaster.TaxAmt =  ;
            }
            Console.WriteLine("after loop....................");

            return View(purchaseImportMasters);
        }



        // GET: PurchaseImportMaster/Create
        public IActionResult Create()
        {
            var model = new PurchaseImportMaster
            {
                // Do not set TransactionID here
                // TransactionID will be set in the POST Create action
            };

            var importers = _context.TaxPayerInfo
                .Where(t => t.BusinessType == "Importer")
                .Select(t => new SelectListItem
                {
                    Value = t.TaxPayerPkid.ToString(),
                    Text = t.PersonName // Display the name instead of ID
                })
                .ToList();

            ViewBag.Importers = importers;

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
                .Include(p => p.PurchaseImportDetail) // Ensure to include the details
                .FirstOrDefaultAsync(p => p.PurchaseImportPkid == id);

            if (purchaseImportMaster == null)
            {
                return NotFound();
            }

            // Debugging
            Console.WriteLine($"Details count: {purchaseImportMaster.PurchaseImportDetail.Count}");

            return View(purchaseImportMaster);
        }



        private bool PurchaseImportMasterExists(int id)
        {
            return _context.PurchaseImportMaster.Any(e => e.PurchaseImportPkid == id);
        }
    }
}
