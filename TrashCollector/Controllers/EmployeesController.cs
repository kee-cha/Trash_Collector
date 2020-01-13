using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;
using System.IO;
using System.Net;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TrashCollector.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext context;
        public EmployeesController()
        {
            context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var employee = context.Employees.ToList();
            return View(employee);
        }
        public EmployeeHomeViewModel Filter(string day)
        {
            string userId = User.Identity.GetUserId();
            Employee employee = context.Employees.Where(e => e.ApplicationId == userId).Single();
            EmployeeHomeViewModel employeeView = new EmployeeHomeViewModel();
            string today = day;
            employeeView.Customers = context.Customers.Include(c=>c.ApplicationUser).Where(c => c.ZipCode == employee.ZipCode && c.PickupDay == today).ToList();
            employeeView.WeekDay = new SelectList(new List<string>() { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" });
            return employeeView;
        }
        // GET: Employees
        public ActionResult GetPickupByDay()
        {  

            string today = DateTime.Now.DayOfWeek.ToString();
            var employeeView = Filter(today);
            return View(employeeView);
        }

        [HttpPost]
        public ActionResult GetPickupByDay(EmployeeHomeViewModel employView)
        {
            var employeeView = Filter(employView.SelectDay);            
            return View(employeeView);
        }
        public ActionResult ConfirmPickup(int? id)
        {

            var customer = context.Customers.Where(c => c.Id == id).SingleOrDefault();
            if (customer.PickupConfirmation == false)
            {
               customer.PickupConfirmation = true;
                customer.Balance += 32.25;
            }
            context.SaveChanges();
            var employeeView = Filter(customer.PickupDay);
            return View("GetPickupByDay", employeeView);
        }
        [HttpPost]
        public ActionResult ConfirmPickup(EmployeeHomeViewModel employView)
        {
            var employeeView = Filter(employView.SelectDay);
            return View("GetPickupByDay", employeeView);
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            try
            {

                // TODO: Add insert logic here
                employee.ApplicationId = User.Identity.GetUserId();
                context.Employees.Add(employee);
                context.SaveChanges();
                return RedirectToAction("LogOut", "Account");
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
