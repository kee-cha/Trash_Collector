using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
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
        public ActionResult Index()
        {
            var customers = context.Customers.ToList();
            return View(customers);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int id)
        {
            Customer customer = context.Customers.Where(c => c.Id == id).SingleOrDefault();
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
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int id)
        {
            Customer customer = context.Customers.Where(c => c.Id == id).SingleOrDefault();
            return View();
        }

        // POST: Customers/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Customer customer)
        {
            try
            {
                // TODO: Add update logic here
                var updateCustomer = context.Customers.Where(c => c.Id == id).SingleOrDefault();
                updateCustomer.FirstName = customer.FirstName;
                updateCustomer.LastName = customer.LastName;
                updateCustomer.PickupConfirmation = customer.PickupConfirmation;
                updateCustomer.PickupDay = customer.PickupDay;
                updateCustomer.ExtraPickupDate = customer.ExtraPickupDate;
                updateCustomer.Balance = customer.Balance;
                updateCustomer.SuspendStart = customer.SuspendStart;
                updateCustomer.SuspendEnd = customer.SuspendEnd;
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

        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            Customer customer = context.Customers.Where(c => c.Id == id).SingleOrDefault();
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
