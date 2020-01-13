using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrashCollector.Models
{
    public class EmployeeHomeViewModel
    {
        public SelectList WeekDay { get; set; }
        public List<Customer> Customers { get; set; }
        public string SelectDay { get; set; }
        public string isPickup { get; set; }
    }
}