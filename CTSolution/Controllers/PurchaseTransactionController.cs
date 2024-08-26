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
        var lastTransaction = _context.PurchaseTransaction
            .Where(t => t.TransactionID == transactionID)
            .OrderByDescending(t => t.TransactionDate)
            .FirstOrDefault();

        decimal? balanceAmt = lastTransaction?.BalanceAmt;

       
        
        
        var model = new PurchaseTransaction
        {
            TransactionID = transactionID,
            TransactionDate = transactionDate,
            TaxOffice = taxOffice,
            TaxOfficer = taxOfficer,
            TaxAmt = taxAmt,
            BalanceAmt = balanceAmt 
        };
        ViewBag.FormattedTransactionDate = transactionDate.ToString("yyyy-MM-ddTHH:mm");

        return View(model);
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

        // Calculate the remaining balance
        decimal previousPayments = _context.PurchaseTransaction
            .Where(pt => pt.TransactionID == transactionID && (pt.IsDeleted == false || pt.IsDeleted == null))
            .Sum(pt => pt.PaidAmt) ?? 0;

        decimal balanceAmt = (decimal)(taxAmt - (previousPayments + purchaseTransaction.PaidAmt));

        purchaseTransaction.BalanceAmt = balanceAmt;

        // Mark as completed if the full amount is paid
        purchaseTransaction.IsCompleted = balanceAmt == 0;

        _context.PurchaseTransaction.Add(purchaseTransaction);
        await _context.SaveChangesAsync();

        // Set TempData message for zero balance
        if (balanceAmt == 0)
        {
            TempData["ZeroBalanceMessage"] = "Remaining Amount to Pay: 0";
        }

        // Redirect to Last Completed Transaction view if completed
        if ((bool)purchaseTransaction.IsCompleted)
        {
            return RedirectToAction("LastCompletedTransaction", new { transactionID = purchaseTransaction.TransactionID });
        }

        return RedirectToAction(nameof(Create), new { transactionID = transactionID, transactionDate = transactionDate, taxOffice = taxOffice, taxOfficer = taxOfficer, taxAmt = taxAmt });
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


    public IActionResult LastCompletedTransaction()
    {
        var lastCompletedTransaction = _context.PurchaseTransaction
            .Where(t => t.BalanceAmt == 0 && t.IsCompleted == true)
            .OrderByDescending(t => t.TransactionDate)
            .ToList();

        return View(lastCompletedTransaction);
    }



}
