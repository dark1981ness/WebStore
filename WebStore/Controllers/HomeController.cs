﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => Content("First Controller Action");

        public IActionResult SecondAction(string id) => Content($"Action with value id:{id}");
    }
}
