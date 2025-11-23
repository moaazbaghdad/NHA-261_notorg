using System.Threading.Tasks;
using STOCKUPMVC.Models;

namespace STOCKUPMVC.Helpers
{
    public static class EmailHelper
    {
        public static async Task SendPurchaseOrderEmail(PurchaseOrder po)
        {
            // You will replace this later with real SMTP service
            await Task.Delay(200);

            System.Diagnostics.Debug.WriteLine(
                $"[EMAIL] PO #{po.POID} sent to {po.Supplier?.Email}");
        }
    }
}
