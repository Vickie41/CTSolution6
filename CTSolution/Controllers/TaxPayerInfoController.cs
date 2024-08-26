using Microsoft.AspNetCore.Mvc;
using CTSolution.Models;
using Microsoft.EntityFrameworkCore;
using CTSolution.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CTSolution.Controllers
{
    public class TaxPayerInfoController : Controller
    {
        private readonly ApplicationDbContextcs _context;

        public TaxPayerInfoController(ApplicationDbContextcs context)
        {
            _context = context;
        }

        // GET: TaxPayerInfo
        public async Task<IActionResult> Index()
        {
            return View(await _context.TaxPayerInfo.ToListAsync());
        }

       
        // GET: TaxPayerInfo/Create
        public IActionResult Create()
        {
            
            var businessTypes = new List<string> { "Importer", "Exporter", "Manufacturer", "Wholesaler", "Retailer"};

           
            ViewData["BusinessTypes"] = new SelectList(businessTypes);

            return View(new TaxPayerInfo());
        }


        // POST: TaxPayerInfo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaxPayerInfo taxPayerInfo)
        {

                try
                {
                    _context.Add(taxPayerInfo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    // Handle exceptions
                    ModelState.AddModelError("", "Unable to save changes. Try again later.");
                }
            ViewData["BusinessTypes"] = TaxPayerInfo.BusinessTypes;
            return View(taxPayerInfo);
        }


        // GET: TaxPayerInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            

            if (id == null)
            {
                return NotFound();
            }

            var taxPayerInfo = await _context.TaxPayerInfo.FindAsync(id);
            if (taxPayerInfo == null)
            {
                return NotFound();
            }

            var businessTypes = new List<string> { "Importer", "Exporter", "Manufacturer", "Wholesaler", "Retailer" };

            ViewData["BusinessTypes"] = new SelectList(businessTypes);

            return View(taxPayerInfo);
        }

        // POST: TaxPayerInfo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaxPayerPkid,PersonName,NRCNo,BusinessName,CompanyName,BizRegNumber,TIN_Number,BusinessType,CommercialLicense,CommercialLicenseDate,PhoneNo,Email,Address,Township,IsDeleted,CreatedBy,CreatedDate")] TaxPayerInfo taxPayerInfo)
        {
            if (id != taxPayerInfo.TaxPayerPkid)
            {
                return NotFound();
            }


                try
                {
                    _context.Update(taxPayerInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaxPayerInfoExists(taxPayerInfo.TaxPayerPkid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));

            var businessTypes = new List<string> {"Importer", "Exporter", "Manufacturer", "Wholesaler", "Retailer" };

            ViewData["BusinessTypes"] = new SelectList(businessTypes);

            return View(taxPayerInfo);
        }

        // GET: TaxPayerInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxPayerInfo = await _context.TaxPayerInfo
                .FirstOrDefaultAsync(m => m.TaxPayerPkid == id);
            if (taxPayerInfo == null)
            {
                return NotFound();
            }

            return View(taxPayerInfo);
        }

        // POST: TaxPayerInfo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taxPayerInfo = await _context.TaxPayerInfo.FindAsync(id);
            if (taxPayerInfo != null)
            {
                _context.TaxPayerInfo.Remove(taxPayerInfo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        // GET: TaxPayerInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxPayerInfo = await _context.TaxPayerInfo
                .FirstOrDefaultAsync(m => m.TaxPayerPkid == id);
            if (taxPayerInfo == null)
            {
                return NotFound();
            }

            return View(taxPayerInfo);
        }

        private bool TaxPayerInfoExists(int id)
        {
            return _context.TaxPayerInfo.Any(e => e.TaxPayerPkid == id);
        }
    }
}
