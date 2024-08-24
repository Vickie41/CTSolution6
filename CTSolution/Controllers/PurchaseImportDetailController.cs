using CTSolution.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class PurchaseImportDetailController : Controller
{
    private readonly ApplicationDbContextcs _context;

    public PurchaseImportDetailController(ApplicationDbContextcs context)
    {
        _context = context;
    }

    // GET: PurchaseImportDetail/Index
    public async Task<IActionResult> Index()
    {
        var purchaseImportDetails = await _context.PurchaseImportDetail
            .Include(d => d.PurchaseImportMaster) // Optional: include related data
            .ToListAsync();
        return View(purchaseImportDetails);
    }

    // GET: PurchaseImportDetail/Create
    public IActionResult Create(string transactionId)
    {
        var model = new PurchaseImportDetail
        {
            TransactionID = transactionId
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PurchaseImportDetail purchaseImportDetail)
    {
        try
        {
            
                // Check if TransactionID exists
                var master = await _context.PurchaseImportMaster
                    .FirstOrDefaultAsync(m => m.TransactionID == purchaseImportDetail.TransactionID);

                if (master == null)
                {
                    ModelState.AddModelError("", "Invalid TransactionID.");
                    return View(purchaseImportDetail);
                }

                // Associate the PurchaseImportMaster
                purchaseImportDetail.PurchaseImportMaster = master;

                _context.Add(purchaseImportDetail);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            
            Console.WriteLine("Model state is not valid.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred: {ex.Message}");
        }
        return View(purchaseImportDetail);
    }



    // GET: PurchaseImportDetail/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var purchaseImportDetail = await _context.PurchaseImportDetail.FindAsync(id);
        if (purchaseImportDetail == null)
        {
            return NotFound();
        }

        return View(purchaseImportDetail);
    }

    // POST: PurchaseImportDetail/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("PurchaseImportDetailPkid,TransactionID,GoodsServicesName,UnitType,Qty,ImportValue,ValueOnLand,TaxRate,TaxAmt,Note")] PurchaseImportDetail purchaseImportDetail)
    {
        if (id != purchaseImportDetail.PurchaseImportDetailPkid)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            try
            {
               
                _context.Attach(purchaseImportDetail);

                
                _context.Update(purchaseImportDetail);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseImportDetailExists(purchaseImportDetail.PurchaseImportDetailPkid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        else
        {
            
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
              
                Console.WriteLine("Here is AAAAAAAAAAAAAAAAAAAA");
            }
        }

        return View(purchaseImportDetail);
    }



    // GET: PurchaseImportDetail/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var purchaseImportDetail = await _context.PurchaseImportDetail
            .FirstOrDefaultAsync(m => m.PurchaseImportDetailPkid == id);
        if (purchaseImportDetail == null)
        {
            return NotFound();
        }

        return View(purchaseImportDetail);
    }

    // POST: PurchaseImportDetail/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var purchaseImportDetail = await _context.PurchaseImportDetail.FindAsync(id);
        _context.PurchaseImportDetail.Remove(purchaseImportDetail);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PurchaseImportDetailExists(int id)
    {
        return _context.PurchaseImportDetail.Any(e => e.PurchaseImportDetailPkid == id);
    }


    // GET: PurchaseImportDetail/Details/5
    public async Task<IActionResult> Detail(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var purchaseImportDetail = await _context.PurchaseImportDetail
            .FirstOrDefaultAsync(m => m.PurchaseImportDetailPkid == id);

        if (purchaseImportDetail == null)
        {
            return NotFound();
        }

        return View(purchaseImportDetail);
    }
}

