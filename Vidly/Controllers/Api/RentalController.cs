using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Dtos;

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        [HttpPost]
        public ActionResult<CustomerDto> NewRental(NewRentalDto newRentalDto)
        {
            throw new NotImplementedException();
        }
    }
}
