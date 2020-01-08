using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext context;
        public EmployeesController()
        {
            context = new ApplicationDbContext();
        }

        // GET: Employees
        public ActionResult Index()
        {   

            var employees = context.Employees.ToList();
            return View(employees);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int id)
        {
            Employee employee = context.Employees.Where(em => em.Id == id).SingleOrDefault();
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            Employee employee = new Employee();
            return View(employee);
        }

        // POST: Employees/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            try
            {

                // TODO: Add insert logic here
                employee.ApplicationId = User.Identity.GetUserId();
                context.Employees.Add(employee);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int id)
        {
            Employee employee = context.Employees.SingleOrDefault(e => e.Id == id);
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee employee)
        {
            try
            {
                // TODO: Add update logic here
                var updateEmployee = context.Employees.SingleOrDefault(e => e.Id == id);
                updateEmployee.FirstName = employee.FirstName;
                updateEmployee.LastName = employee.LastName;
                updateEmployee.ZipCode = employee.ZipCode;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employees/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
