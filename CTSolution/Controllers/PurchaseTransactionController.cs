using CTSolution.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class PurchaseTransactionController : Controller
{
    private readonly ApplicationDbContextcs _context;

    public PurchaseTransactionController(ApplicationDbContextcs context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string transactionID, DateTime transactionDate, string taxOffice, string taxOfficer, decimal taxAmt)
    {
        ViewBag.TransactionID = transactionID;
        ViewBag.TransactionDate = transactionDate;
        ViewBag.TaxOffice = taxOffice;
        ViewBag.TaxOfficer = taxOfficer;
        ViewBag.TaxAmt = taxAmt;
        return View(await _context.PurchaseTransaction.ToListAsync());
    }


    // GET: PurchaseTransaction/Create
    public IActionResult Create(string transactionID, DateTime transactionDate, string taxOffice, string taxOfficer, decimal taxAmt)
    {
        
        
        var Model = new PurchaseTransaction
        {
            TransactionID = transactionID,
            TransactionDate = transactionDate,
            TaxOffice = taxOffice,
            TaxOfficer = taxOfficer,
            TaxAmt = taxAmt
        };

        return View(Model);
    }

    // POST: PurchaseTransaction/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ChalenNo,PaidAmt,Note")] PurchaseTransaction purchaseTransaction, string transactionID, DateTime transactionDate, string taxOffice, string taxOfficer, decimal taxAmt)
    {

        
            purchaseTransaction.TransactionID = transactionID;
            purchaseTransaction.TransactionDate = transactionDate;
            purchaseTransaction.TaxOffice = taxOffice;
            purchaseTransaction.TaxOfficer = taxOfficer;
            purchaseTransaction.TaxAmt = taxAmt;
            _context.PurchaseTransaction.Add(purchaseTransaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
    }



        // GET: PurchaseTransaction/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var purchaseTransaction = await _context.PurchaseTransaction
                .FirstOrDefaultAsync(m => m.TransactionID == id);
            if (purchaseTransaction == null)
            {
                return NotFound();
            }

            return View(purchaseTransaction);
        }

    // POST: PurchaseTransaction/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var purchaseTransaction = await _context.PurchaseTransaction
            .FirstOrDefaultAsync(m => m.TransactionID == id);

        if (purchaseTransaction != null)
        {
            _context.PurchaseTransaction.Remove(purchaseTransaction);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }


}
