﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FPTBook.Data;
using FPTBook.Models;
using Microsoft.AspNetCore.Authorization;
using FPTBook.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace FPTBook.Controllers
{
   // [Authorize(Roles ="Seller")]
    public class OrderDetailsController : Controller
    {
        private readonly FPTBookContext _context;
        private readonly UserManager<FPTBookUser> _userManager;

        public OrderDetailsController(FPTBookContext context, UserManager<FPTBookUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Authorize(Roles = "Customer")]
        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
            string thisUsers = _userManager.GetUserId(HttpContext.User);
            var fPTBookContext = _context.OrderDetail.Where( o => o.Order.UId== thisUsers).Include(o => o.Book).Include(o => o.Order).Include(o=>o.Order.User);
            return View(await fPTBookContext.ToListAsync());
        }
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> orderManager()
        {
            var orderManager = _context.OrderDetail.Include(o => o.Book).Include(o => o.Order).Include(o => o.Order.User);
            return View(await orderManager.ToListAsync());
        }

       

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetail.Any(e => e.OrderId == id);
        }
    }
}
