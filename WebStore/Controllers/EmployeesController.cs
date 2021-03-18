using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly List<Employee> _Employees;

        public EmployeesController()
        {
            _Employees = TestData.Employees;
        }
        public IActionResult Index() => View(_Employees);

        public IActionResult Details(int id)
        {
            Employee employee = _Employees.SingleOrDefault(s => s.Id == id);
            if (employee is null)
            {
                return NotFound();
            }
            return View(employee);
        }
    }
}
