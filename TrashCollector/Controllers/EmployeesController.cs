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
            string userId = User.Identity.GetUserId();
            Employee employee = context.Employees.Where(e => e.ApplicationId == userId).Single();
            EmployeeHomeViewModel employeeView = new EmployeeHomeViewModel();
            employeeView.Customers = context.Customers.Where(c => c.ZipCode == employee.ZipCode).ToList();
            employeeView.WeekDay = new SelectList(new List<string>() { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" });
            return View(employeeView);
        }

        [HttpPost]
        public ActionResult GetPickupByDay(EmployeeHomeViewModel employView)
        {
            string userId = User.Identity.GetUserId();
            Employee employee = context.Employees.Where(e => e.ApplicationId == userId).Single();
            EmployeeHomeViewModel employeeView = new EmployeeHomeViewModel();
            employeeView.Customers = context.Customers.Where(c => c.ZipCode == employee.ZipCode).ToList();
            employeeView.WeekDay = new SelectList(new List<string>() { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" });
            return View(employeeView)
        }
        public ActionResult ChargeBalance()
        {
            var people = context.Customers.Select(c => c).ToList();
            string dateTime = DateTime.Today.DayOfWeek.ToString();
            int time = DateTime.Now.Hour;
            foreach (var item in people)
            {
                if (item.PickupConfirmation == true && item.PickupDay == dateTime && time >= 0)
                {
                    item.Balance += 35;
                    item.PickupConfirmation = false;
                }
            }
            context.SaveChanges();
            return RedirectToAction("CustomerIndex");
        }
        // GET: Employees/Details/5
        public ActionResult Details(int id)
        {
            var employee = context.Employees.Where(em => em.Id == id).SingleOrDefault();
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
            var employee = context.Employees.SingleOrDefault(e => e.Id == id);
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
            var employee = context.Employees.Where(e => e.Id == id).SingleOrDefault();
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Employee employee)
        {
            try
            {
                // TODO: Add delete logic here
                employee = context.Employees.Where(e => e.Id == id).SingleOrDefault();
                var removeEmpUser = context.Users.Where(e => e.Id == employee.ApplicationId).SingleOrDefault();
                context.Users.Remove(removeEmpUser);
                context.Employees.Remove(employee);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
