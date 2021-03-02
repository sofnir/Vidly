using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class CustomerFormViewModel
    {
        public ICollection<MembershipType> MembershipTypes { get; set; }
        public Customer Customer { get; set; }
    }
}
