using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class CustomersController : Controller
    {
        ApplicationDbContext context;
        public CustomersController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Customers

        public ActionResult Index(int id)
        {
            var customers = context.Customers.Include(c => c.ApplicationUser).Where(c => c.Id == id).ToList();
            return View(customers);
        }
        // GET: Customers

        public ActionResult CustomerIndex()
        {

            var id = User.Identity.GetUserId();
            var currentEmployee = context.Employees.Where(e => e.ApplicationId == id).SingleOrDefault();
            var customers = context.Customers.Include(c => c.ApplicationUser).Where(c => c.ZipCode == currentEmployee.ZipCode).ToList();

            return View(customers);
        }
        public ActionResult GetCustomerByDay()
        {
            DateTime today = DateTime.Now;
            string currentDay = today.DayOfWeek.ToString();
            var id = User.Identity.GetUserId();
            var currentEmployee = context.Employees.Where(e => e.ApplicationId == id).SingleOrDefault();
            var customer = context.Customers.Include(c => c.ApplicationUser).Where(c => c.PickupDay == currentDay && c.ZipCode == currentEmployee.ZipCode).ToList();
            return View("CustomerIndex", customer);
        }
        // GET: Customers/Details/5
        public ActionResult Details(int? ID)
        {
            if (User.IsInRole("Employee")!=true)
            {
                var id = User.Identity.GetUserId();
                var customer = context.Customers.Include(c => c.ApplicationUser).Where(c => c.ApplicationId == id).SingleOrDefault();
                return View(customer);
            }
            else
            {
                var person = context.Customers.Include(c => c.ApplicationUser).Where(c => c.Id == ID).SingleOrDefault();
                return View(person);
            }
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            Customer customer = new Customer();
            return View(customer);
        }

        // POST: Customers/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                // TODO: Add insert logic here                
                customer.ApplicationId = User.Identity.GetUserId();
                context.Customers.Add(customer);
                context.SaveChanges();
                return RedirectToAction("Details",customer);
            }
            catch
            {
                return View();
            }
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int id)
        {
            var customer = context.Customers.Include(c => c.ApplicationUser).Where(c => c.Id == id).SingleOrDefault();
            return View();
        }

        // POST: Customers/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Customer customer)
        {
            try
            {
                // TODO: Add update logic here
                var updateCustomer = context.Customers.Include(c => c.ApplicationUser).Where(c => c.Id == id).SingleOrDefault();
                updateCustomer.FirstName = customer.FirstName;
                updateCustomer.LastName = customer.LastName;
                updateCustomer.ApplicationUser.UserName = customer.ApplicationUser.UserName;
                updateCustomer.ApplicationUser.Email = customer.ApplicationUser.Email;
                updateCustomer.City = customer.City;
                updateCustomer.State = customer.State;
                updateCustomer.StreetAddress = customer.StreetAddress;
                updateCustomer.ZipCode = customer.ZipCode;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET: Customers/Edit/5
        public ActionResult CreatePickup(int id)
        {
            var customer = context.Customers.Where(c => c.Id == id).SingleOrDefault();
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        public ActionResult CreatePickup(int id, Customer customer)
        {
            try
            {
                var updateCustomer = context.Customers.Where(c => c.Id == id).SingleOrDefault();
                if (User.IsInRole("Employee"))
                {
                    updateCustomer.PickupConfirmation = customer.PickupConfirmation;
                }
                else
                {
                    updateCustomer.PickupDay = customer.PickupDay;
                    updateCustomer.ExtraPickupDate = customer.ExtraPickupDate;
                    updateCustomer.SuspendStart = customer.SuspendStart;
                    updateCustomer.SuspendEnd = customer.SuspendEnd;
                }

                context.SaveChanges();
                return RedirectToAction("Details", updateCustomer);
            }
            catch
            {
                return View();
            }
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

        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            var customer = context.Customers.Where(c => c.Id == id).SingleOrDefault();
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Customer customer)
        {
            try
            {
                // TODO: Add delete logic here
                var deleteCustomer = context.Customers.Where(c => c.Id == id).SingleOrDefault();
                var removeUser = context.Users.Where(r => r.Id == deleteCustomer.ApplicationId).SingleOrDefault();
                context.Users.Remove(removeUser);
                context.Customers.Remove(deleteCustomer);

                context.SaveChanges();
                return RedirectToAction("CustomerIndex");
            }
            catch
            {
                return View();
            }
        }

    }
}
