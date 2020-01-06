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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PickupDay { get; set; }
        public int ZipCode { get; set; }
        public double Balance { get; set; }
        public bool PickupConfirmation { get; set; }
        public DateTime SuspendStart { get; set; }
        public DateTime SuspendEnd { get; set; }
        public DateTime ExtraPickupDate { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        
    }
}