﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookShopping.Data;
using BookShopping.Models;
using Microsoft.AspNetCore.Authorization;
using BookShopping.Models.DTOs;

namespace BookShopping.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository cartRepo;

        public CartController(ICartRepository cartRepo)
        {
            this.cartRepo = cartRepo;
        }
      
        public async Task<IActionResult> AddItem(int bookId, int qty = 1, int Redirect = 0)
        {
            var cartCount = await cartRepo.AddItem(bookId, qty);
            if (Redirect == 0)
                return Ok(cartCount);
            return RedirectToAction("GetUserCart");


        }
        [HttpPost]
        public async Task<IActionResult> RemoveItem(int bookId)
        {
            var cartCount = await cartRepo.RemoveItem(bookId);
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> GetUserCart()
        {
            var cart = await cartRepo.GetCarts();
            return View(cart);
        }
        public async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await cartRepo.GetCartCount();
            return Ok(cartItem);
        }
        public IActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutModel model)
        {
            if (ModelState.IsValid) 
                return View(model);
            bool isCheckedOut = await cartRepo.DoCheckout(model);
            if (!isCheckedOut)
               return RedirectToAction(nameof(OrderFailure));
            return RedirectToAction(nameof(OrderSuccess));

        }

        public IActionResult OrderSuccess()
        {
            return View();
        }

        public IActionResult OrderFailure()
        {
            return View();
        }
    }
}
