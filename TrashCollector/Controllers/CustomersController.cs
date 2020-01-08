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
            var customers = context.Customers.Include(c=>c.ApplicationUser).Where(c=>c.Id==id).ToList();
            return View(customers);
        }
        // GET: Customers

        public ActionResult CustomerIndex()
        {

            var customers = context.Customers.Include(c=>c.ApplicationUser).ToList();
            return View(customers);
        }
        // GET: Customers/Details/5
        public ActionResult Details()
        {
            var id = User.Identity.GetUserId();
            var customer = context.Customers.Include(c=>c.ApplicationUser).Where(c => c.ApplicationId == id).SingleOrDefault();
            return View(customer);
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
                return RedirectToAction("Details");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int id)
        {
            var customer = context.Customers.Include(c=>c.ApplicationUser).Where(c => c.Id == id).SingleOrDefault();
            return View();
        }

        // POST: Customers/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Customer customer)
        {
            try
            {
                // TODO: Add update logic here
                var updateCustomer = context.Customers.Include(c=>c.ApplicationUser).Where(c => c.Id == id).SingleOrDefault();
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
                // TODO: Add update logic here
                var updateCustomer = context.Customers.Where(c => c.Id == id).SingleOrDefault();
                updateCustomer.PickupConfirmation = customer.PickupConfirmation;
                updateCustomer.PickupDay = customer.PickupDay;
                updateCustomer.ExtraPickupDate = customer.ExtraPickupDate;
                updateCustomer.Balance = customer.Balance;
                updateCustomer.SuspendStart = customer.SuspendStart;
                updateCustomer.SuspendEnd = customer.SuspendEnd;            
                context.SaveChanges();
                return RedirectToAction("Details");
            }
            catch
            {
                return View();
            }
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
                customer = context.Customers.Where(c => c.Id == id).SingleOrDefault();
                var removeUser = context.Users.Where(r => r.Id == customer.ApplicationId).SingleOrDefault();
                context.Users.Remove(removeUser);
                context.Customers.Remove(customer);
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
