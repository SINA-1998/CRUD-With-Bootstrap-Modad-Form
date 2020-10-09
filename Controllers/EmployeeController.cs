using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using CRUDBootstrapModadForm.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDBootstrapModadForm.Controllers
{
    [Route("employee")]
    public class EmployeeController : Controller
    {
        private ApplicationDbContext db;

        public EmployeeController(ApplicationDbContext _db)
        {
            db = _db;
        }

        [Route("")]
        [Route("~/")]
        [Route("index")]
        public IActionResult Index()
        {
            ViewBag.employees = db.Employees.ToList();
            return View();
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(string name, string email, string address, string phone)
        {
            var employees = new Employee()
            {
                Name = name,
                Email = email,
                Address = address,
                Phone = phone
            };
            db.Employees.Add(employees);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("remove")]
        public IActionResult Remove(int id)
        {
            var employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("find/{id}")]
        public IActionResult Find(int id)
        {
            var employee = db.Employees.Find(id);
            return new JsonResult(employee);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update(int id, string name, string email, string address, string phone)
        {
            var employee = db.Employees.Find(id);
            employee.Name = name;
            employee.Email = email;
            employee.Address = address;
            employee.Phone = phone;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
