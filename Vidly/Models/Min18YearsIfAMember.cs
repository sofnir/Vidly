using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Dtos;

namespace Vidly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            dynamic customer;
            if(validationContext.ObjectInstance is Customer)
            {
                customer = validationContext.ObjectInstance as Customer;
            }
            else
            {
                customer = validationContext.ObjectInstance as CustomerDto;
            }            

            if (customer.MembershipTypeId == MembershipType.Unknown ||
                customer.MembershipTypeId == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }                

            if (!customer.Birthdate.HasValue)
                return new ValidationResult("Birthdate is required.");

            var age = DateTime.Now.Year - customer.Birthdate.Value.Year;

            return (age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("Birthdate should be at least 18 years to go on a membership.");
        }
    }
}
