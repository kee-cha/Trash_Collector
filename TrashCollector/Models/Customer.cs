using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Pickup Day")]
        public DayOfWeek PickupDay { get; set; }
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "State")]
        public string  State { get; set; }
        [Display(Name = "First Name")]
        public string ZipCode { get; set; }
        [Display(Name = "Balance Due")]
        public double Balance { get; set; }
        [Display(Name = "Confirm Pickup")]
        public bool PickupConfirmation { get; set; }
        [Display(Name = "Suspend Pickup Start Date")]
        public DateTime SuspendStart { get; set; }
        [Display(Name = "Suspend Pickup End Date")]
        public DateTime SuspendEnd { get; set; }
        [Display(Name = "First Name")]
        public DateTime ExtraPickupDate { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }

    }
}