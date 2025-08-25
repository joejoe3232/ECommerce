using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerceApp.Data;
using ECommerceApp.Models;

namespace ECommerceApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Products = await _context.Products.ToListAsync();
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerName,CustomerEmail,CustomerPhone,ShippingAddress,Notes")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.OrderNumber = GenerateOrderNumber();
                order.OrderDate = DateTime.Now;
                order.Status = OrderStatus.Pending;
                order.TotalAmount = 0; // 將在添加訂單項目後計算

                _context.Add(order);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Details), new { id = order.Id });
            }

            ViewBag.Products = await _context.Products.ToListAsync();
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderNumber,CustomerName,CustomerEmail,CustomerPhone,ShippingAddress,Notes,TotalAmount,Status,OrderDate,ShippedDate,DeliveredDate")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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

            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Orders/UpdateStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            order.Status = status;

            switch (status)
            {
                case OrderStatus.Shipped:
                    order.ShippedDate = DateTime.Now;
                    break;
                case OrderStatus.Delivered:
                    order.DeliveredDate = DateTime.Now;
                    break;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = id });
        }

        // POST: Orders/AddOrderItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrderItem(int orderId, int productId, int quantity)
        {
            var order = await _context.Orders.FindAsync(orderId);
            var product = await _context.Products.FindAsync(productId);

            if (order == null || product == null)
            {
                return NotFound();
            }

            var orderItem = new OrderItem
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity,
                UnitPrice = product.Price
            };

            _context.OrderItems.Add(orderItem);

            // 更新訂單總金額
            order.TotalAmount += (product.Price * quantity);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = orderId });
        }

        // POST: Orders/RemoveOrderItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveOrderItem(int orderItemId)
        {
            var orderItem = await _context.OrderItems
                .Include(oi => oi.Order)
                .FirstOrDefaultAsync(oi => oi.Id == orderItemId);

            if (orderItem == null)
            {
                return NotFound();
            }

            var order = orderItem.Order;
            if (order != null)
            {
                order.TotalAmount -= (orderItem.UnitPrice * orderItem.Quantity);
                _context.OrderItems.Remove(orderItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = order.Id });
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        private string GenerateOrderNumber()
        {
            return "ORD-" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
        }
    }
}
