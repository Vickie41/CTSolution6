namespace CTSolution.Models
{
    public class DashboardViewModel
    {
        public int TotalTransactions { get; set; }
        public decimal TotalTaxCollected { get; set; }
        public int RecentActivityCount { get; set; }
        public IEnumerable<RecentTransaction> RecentTransactions { get; set; }
    }

    public class RecentTransaction
    {
        public string TransactionId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}

