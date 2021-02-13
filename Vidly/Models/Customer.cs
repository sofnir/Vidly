using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? Birthdate { get; set; }
        public byte MembershipTypeId { get; set; }

        public MembershipType MembershipType { get; set; }
    }
}
