using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using STOCKUPMVC.Data.Repositories;
using STOCKUPMVC.Models;
using STOCKUPMVC.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace STOCKUPMVC.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class PurchaseOrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseOrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: PurchaseOrder/List
        public async Task<IActionResult> List()
        {
            var orders = await _unitOfWork.PurchaseOrders
                .GetAllQueryable()
                .Include(p => p.Supplier)
                .Include(p => p.Warehouse)
                .Include(p => p.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

            var viewModel = new PurchaseOrderListViewModel
            {
                PurchaseOrders = orders.Select(p => new PurchaseOrderListItemViewModel
                {
                    POID = p.POID,
                    SupplierName = p.Supplier?.Name,
                    WarehouseName = p.Warehouse?.Name,
                    Status = p.Status,
                    TotalAmount = p.TotalAmount,
                    OrderTime = p.OrderTime
                }).ToList()
            };

            return View(viewModel);
        }

        // GET: PurchaseOrder/Create
        // GET: Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Suppliers = new SelectList(await _unitOfWork.Suppliers.GetAllAsync(), "SupplierID", "Name");
            ViewBag.Categories = new SelectList(await _unitOfWork.Categories.GetAllAsync(), "CategoryID", "Name");
            ViewBag.Products = await _unitOfWork.Products.GetAllAsync();
            return View(new PurchaseOrderCreateVMM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseOrderCreateVMM model)
        {
            if (!ModelState.IsValid || model.OrderItems.Count == 0)
            {
                ViewBag.Suppliers = new SelectList(await _unitOfWork.Suppliers.GetAllAsync(), "SupplierID", "Name");
                ViewBag.Categories = new SelectList(await _unitOfWork.Categories.GetAllAsync(), "CategoryID", "Name");
                ViewBag.Products = await _unitOfWork.Products.GetAllAsync();
                return View(model);
            }

            var po = new PurchaseOrder
            {
                SupplierID = model.SupplierID,
                OrderTime = model.OrderTime,
                TotalAmount = model.TotalAmount,
                Status = "Pending",
                OrderItems = model.OrderItems.Select(i => new OrderItem
                {
                    ProductID = i.ProductID,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            await _unitOfWork.PurchaseOrders.AddAsync(po);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction("Index");
        }

        // For AJAX: Get products by category
        public async Task<JsonResult> GetProductsByCategory(int categoryId)
        {
            var products = (await _unitOfWork.Products.FindAsync(p => p.CategoryID == categoryId))
                           .Select(p => new { p.ProductID, p.Name, p.Price });
            return Json(products);
        }
    }
}
