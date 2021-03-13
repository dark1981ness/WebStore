﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<Employee> _employees = new()
        {
            new Employee { Id=1,LastName="Иванов",FirstName="Иван",Patronymic="Иванович", Age=27},
            new Employee { Id=2,LastName="Петров",FirstName="Петр",Patronymic="Петрович", Age=31},
            new Employee { Id=3,LastName="Сидоров",FirstName="Сидор",Patronymic="Сидорович", Age=18}
        };

        public IActionResult Index() => View();

        public IActionResult SecondAction(string id) => Content($"Action with value id:{id}");

        public IActionResult Employees()
        {
            return View(_employees);
        }
    }
}
