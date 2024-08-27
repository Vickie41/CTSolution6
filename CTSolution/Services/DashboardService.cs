using CTSolution.Models;
using Microsoft.EntityFrameworkCore;

namespace CTSolution.Services
{
    public class DashboardService
    {
        private readonly ApplicationDbContextcs _context;

        public DashboardService(ApplicationDbContextcs context)
        {
            _context = context;
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync()
        {
            var totalTransactions = await _context.PurchaseTransaction.CountAsync();
            var totalTaxCollected = await _context.PurchaseTransaction
                .SumAsync(t => t.PaidAmt ?? 0); // Handle nullable decimal

            var recentTransactions = await _context.PurchaseTransaction
                .OrderByDescending(t => t.TransactionDate)
                .Take(5)
                .Select(t => new RecentTransaction
                {
                    // Assuming TransactionID is a string and not nullable
                    TransactionId = string.IsNullOrEmpty(t.TransactionID) ? "N/A" : t.TransactionID,
                    Date = t.TransactionDate ?? DateTime.MinValue, // Handle nullable DateTime
                    Amount = t.PaidAmt ?? 0 // Handle nullable decimal
                })
                .ToListAsync();

            return new DashboardViewModel
            {
                TotalTransactions = totalTransactions,
                TotalTaxCollected = totalTaxCollected,
                RecentActivityCount = recentTransactions.Count(),
                RecentTransactions = recentTransactions
            };
        }
    }

}

