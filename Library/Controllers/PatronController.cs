﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models.Patron;
using LibraryData;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class PatronController : Controller
    {
        private readonly IPatronService _patronService;

        public PatronController(IPatronService patronService)
        {
            _patronService = patronService;
        }

        public IActionResult Index()
        {
            var allPatrons = _patronService.GetAll();

            var patronModels = allPatrons
                .Select(p => new PatronDetailModel
                {
                    Id = p.Id,
                    LastName = p.LastName ?? "No First Name Provided",
                    FirstName = p.FirstName ?? "No Last Name Provided",
                    LibraryCardId = p.LibraryCard?.Id,
                    OverdueFees = p.LibraryCard?.Fees,
                    HomeLibrary = p.HomeLibraryBranch?.Name
                }).ToList();

            var model = new PatronIndexModel
            {
                Patrons = patronModels
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var patron = _patronService.Get(id);

            var model = new PatronDetailModel
            {
                Id = patron.Id,
                LastName = patron.LastName ?? "No Last Name Provided",
                FirstName = patron.FirstName ?? "No First Name Provided",
                Address = patron.Address ?? "No Address Provided",
                HomeLibrary = patron.HomeLibraryBranch?.Name ?? "No Home Library",
                MemberSince = patron.LibraryCard?.Created,
                OverdueFees = patron.LibraryCard?.Fees,
                LibraryCardId = patron.LibraryCard?.Id,
                TelephoneNumber = string.IsNullOrEmpty(patron.TelephoneNumber) ? "No Telephone Number Provided" : patron.TelephoneNumber,
                AssetsCheckedOut = _patronService.GetCheckouts(id).ToList(),
                CheckoutHistory = _patronService.GetCheckoutHistory(id),
                Holds = _patronService.GetHolds(id)
            };

            return View(model);
        }
    }
}