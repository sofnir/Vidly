using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class CustomerFormViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Please enter customer's name.")]
        [MaxLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        [Column(TypeName = "Date")]
        [Display(Name = "Date of Birthday")]
        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }

        [Display(Name = "Membership Type")]
        public byte? MembershipTypeId { get; set; }        

        public ICollection<MembershipType> MembershipTypes { get; set; }

        public CustomerFormViewModel()
        {
            Id = 0;
        }

        public CustomerFormViewModel(Customer customer)
        {
            Id = customer.Id;
            Name = customer.Name;
            IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            Birthdate = customer.Birthdate;
            MembershipTypeId = customer.MembershipTypeId;
        }
    }
}
