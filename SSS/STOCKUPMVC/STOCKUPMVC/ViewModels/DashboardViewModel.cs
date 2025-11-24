using STOCKUPMVC.Models;

namespace STOCKUPMVC.ViewModels
{
    public class DashboardViewModel
    {
        public int ProductCount { get; set; }
        public int WarehouseCount { get; set; }
        public int PendingSalesOrderCount { get; set; }

        public List<SalesOrder> RecentSalesOrders { get; set; }
    }
}
