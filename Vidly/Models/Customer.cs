using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Vidly.Models;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {


        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public String Name { get; set; }
        [Display (Name = "Date Of Birth")]
        public DateTime? BirthDate { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }
        public MemberShipType MemberShipType { get; set; }
        [Display (Name="Membership Type")]
        public byte MemberShipTypeId { get; set; }
    }
}