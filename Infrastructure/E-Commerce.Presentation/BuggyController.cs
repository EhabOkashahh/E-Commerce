using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase
    {
        [HttpGet("notfound")]
        public IActionResult GetNotFoundResponse()
        {
            return NotFound();
        }


        [HttpGet("badrequest")]
        public IActionResult GetBadRequestResponse()
        {
            return BadRequest();
        }


        [HttpGet("badrequest/{id}")]
        public IActionResult GetValidationErrorResponse(int id)
        {
            return BadRequest(id);
        }


        [HttpGet("servererror")]
        public IActionResult GetServerErrorResponse()
        {
            throw new Exception();
        }


        [HttpGet("unauthorized")]
        public IActionResult GetUnAuthorizedResponse()
        {
            return Unauthorized();
        }
    }
}
