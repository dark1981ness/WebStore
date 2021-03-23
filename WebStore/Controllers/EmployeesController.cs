using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Controllers
{
    [Route("Staff")]
    public class EmployeesController : Controller
    {
        private readonly List<Employee> _Employees;

        public EmployeesController()
        {
            _Employees = TestData.Employees;
        }

        [Route("all")]
        public IActionResult Index() => View(_Employees);

        [Route("info-(id-{id})")]
        public IActionResult Details(int id)
        {
            Employee employee = _Employees.SingleOrDefault(s => s.Id == id);
            if (employee is null)
            {
                return NotFound();
            }
            return View(employee);
        }

        public IActionResult Edit(int id)
        {
            Employee employee = _Employees.SingleOrDefault(s => s.Id == id);
            if (employee is null)
            {
                return NotFound();
            }
            return View(employee);
        }

        public IActionResult Delete(int id)
        {
            Employee employee = _Employees.SingleOrDefault(s => s.Id == id);
            _Employees.Remove(employee);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Employee employee, int id)
        {
            _Employees.Where(s => s.Id == id)
                .Select(s =>
                {
                    s.FirstName = employee.FirstName;
                    s.LastName = employee.LastName;
                    s.Patronymic = employee.Patronymic;
                    s.Birthday = employee.Birthday;
                    s.HireDate = employee.HireDate;
                    s.Salary = employee.Salary;
                    s.EMail = employee.EMail;
                    return s;
                }).ToList();

            return RedirectToAction("Index");
        }
    }
}
