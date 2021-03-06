﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApp.Infrastructure.Inherits;

namespace WebApp.Features.Admin.Dashboard
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class DashboardController : Controller
    {
        /// <summary>
        /// Administrator's Dashboard
        /// </summary>
        /// <returns>Administrator's Dashboard View</returns>
        [Route("/Admin")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Dashboard"; //The title of the page must be passed through with each action method
            var conn = Environment.GetEnvironmentVariable("BUHCS");
            /*
             * The name of the cshtml file in the same folder as the controller
             * In this case: /Features/Admin/Dashboard/Dashboard.cshtml
             * Where:        /Features/{area}/{controller}/{View() string argument}
             */
            return View("Dashboard", conn);
        }
    }
}