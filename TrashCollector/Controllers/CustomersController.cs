using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;
using System.Xml.Linq;
using System.Net;

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


        // GET: Customers/Details/5
        public ActionResult Details(int? ID)
        {
            if (User.IsInRole("Employee") != true)
            {
                var id = User.Identity.GetUserId();
                var customer = context.Customers.Include(c => c.ApplicationUser).Where(c => c.ApplicationId == id).SingleOrDefault();
                return View(customer);
            }
            else
            {
                var person = context.Customers.Include(c => c.ApplicationUser).Where(c => c.Id == ID).SingleOrDefault();
                var location = person.StreetAddress + "," + person.City + "," + person.State;
                var requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?key={1}&address={0}&sensor=false", Uri.EscapeDataString(location), GoogleMapKey.myKey);
                WebRequest request = WebRequest.Create(requestUri);
                WebResponse response = request.GetResponse();
                XDocument xDoc = XDocument.Load(response.GetResponseStream());
                XElement result = xDoc.Element("GeocodeResponse").Element("result");
                if (result != null)
                {
                    XElement locationElement = result.Element("geometry").Element("location");
                    XElement lat = locationElement.Element("lat");
                    XElement lng = locationElement.Element("lng");
                    var latCoord = lat.Value;
                    var lngCoord = lng.Value;
                    string map = string.Format("https://maps.googleapis.com/maps/api/staticmap?center={0}&zoom=13&size=600x300&maptype=roadmap&markers=color:red%7C{2},{3}&key={1}", Uri.EscapeDataString(location), GoogleMapKey.myKey, latCoord, lngCoord);
                    ViewBag.getMap = map;
                }
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
                return RedirectToAction("LogOut", "Account");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            var customer = context.Customers.Include(c => c.ApplicationUser).Where(c => c.Id == id).SingleOrDefault();
            return View();
        }

        // POST: Customers/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, Customer customer)
        {
            try
            {
                // TODO: Add update logic here
                var updateCustomer = context.Customers.Include(c=>c.ApplicationUser).Where(c => c.Id == id).SingleOrDefault();
                var userNameHolder = updateCustomer.ApplicationUser.UserName;
                var emailHolder = updateCustomer.ApplicationUser.Email;
                updateCustomer.FirstName = customer.FirstName;
                updateCustomer.LastName = customer.LastName;
                updateCustomer.ApplicationUser.UserName = customer.ApplicationUser.UserName;
                updateCustomer.ApplicationUser.Email = customer.ApplicationUser.Email;
                updateCustomer.City = customer.City;
                updateCustomer.State = customer.State;
                updateCustomer.StreetAddress = customer.StreetAddress;
                updateCustomer.ZipCode = customer.ZipCode;
                if (customer.ApplicationUser.UserName == null)
                {
                    updateCustomer.ApplicationUser.UserName = userNameHolder;
                }
                if (customer.ApplicationUser.Email==null)
                {
                    updateCustomer.ApplicationUser.Email = emailHolder;
                }
                context.SaveChanges();
                return RedirectToAction("Details", updateCustomer);
            }
            catch(Exception e)
            {
                return View();
            }
        }
        // GET: Customers/Edit/5
        public ActionResult CreatePickup(int? id)
        {
            var customer = context.Customers.Where(c => c.Id == id).SingleOrDefault();
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        public ActionResult CreatePickup(int? id, Customer customer)
        {
            try
            {
                var updateCustomer = context.Customers.Where(c => c.Id == id).SingleOrDefault();
                updateCustomer.PickupDay = customer.PickupDay;
                updateCustomer.ExtraPickupDate = customer.ExtraPickupDate;
                updateCustomer.SuspendStart = customer.SuspendStart;
                updateCustomer.SuspendEnd = customer.SuspendEnd;
                context.SaveChanges();
                return RedirectToAction("Details", updateCustomer);
            }
            catch
            {
                return View();
            }
        }


        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            var customer = context.Customers.Where(c => c.Id == id).SingleOrDefault();
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, Customer customer)
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
