using Microsoft.AspNetCore.Mvc;
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
            new Employee
            {
                Id = 1,
                LastName = "Иванов",
                FirstName = "Иван",
                Patronymic = "Иванович",
                Birthday = new DateTime(1975, 10, 28),
                HireDate = new DateTime(2015, 04, 1),
                EMail = @"IIvanov@gb.ru",
                Salary = 50000
            },
            new Employee
            {
                Id = 2,
                LastName = "Петров",
                FirstName = "Петр",
                Patronymic = "Петрович",
                Birthday = new DateTime (1995, 01, 30),
                HireDate = new DateTime (2015, 04, 1),
                EMail = @"PPetrov@gb.ru",
                Salary = 60000
            },
            new Employee
            {
                Id = 3,
                LastName = "Сидоров",
                FirstName = "Сидор",
                Patronymic = "Сидорович",
                Birthday = new DateTime (1986, 08, 15),
                HireDate = new DateTime (2015, 04, 1),
                EMail = @"SSidorov@gb.ru",
                Salary = 150000
            }
        };

        public IActionResult Index() => View();

        public IActionResult SecondAction(string id) => Content($"Action with value id:{id}");

        public IActionResult Employees()
        {
            return View(_employees);
        }

        public IActionResult EmployeeCard(int id)
        {
            Employee employee = _employees.SingleOrDefault(s => s.Id == id);
            return View(employee);
        }
    }
}
