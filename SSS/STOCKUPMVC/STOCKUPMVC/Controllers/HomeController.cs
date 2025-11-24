using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using STOCKUPMVC.Data.Repositories;
using STOCKUPMVC.Models;
using STOCKUPMVC.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace STOCKUPMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("Admin") || roles.Contains("Staff"))
                {
                    var vm = new DashboardViewModel
                    {
                        ProductCount = await _unitOfWork.Products.CountAsync(),
                        WarehouseCount = await _unitOfWork.Warehouses.CountAsync(),
                        PendingSalesOrderCount = await _unitOfWork.SalesOrders.CountAsync(s => s.Status == "Pending"),

                        RecentSalesOrders = _unitOfWork.SalesOrders
                            .GetAllQueryable()
                            .OrderByDescending(s => s.OrderDate)
                            .Take(5)
                            .ToList()
                    };

                    return View("AdminView", vm);
                }
            }

            return View("UserView");
        }

    }
}
