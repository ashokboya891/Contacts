using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class BuggyController:BaseApiController
    {
        private readonly BikesDbContext _bikesDbContext;
        public BuggyController(BikesDbContext bikesDbContext)
        {
            _bikesDbContext = bikesDbContext;

        }
        [Authorize]
        [HttpGet("testauth")]
        public ActionResult<string> GetSecret()
        {
            return "secret";
        }
        
        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing=_bikesDbContext.tblBikes.Find(42);
            if(thing==null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }
        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var thing=_bikesDbContext.tblBikes.Find(42);

            var thingToRetun=thing.ToString();

            return Ok();
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
            // return BadRequest();
        }
        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {

            return Ok();
        }
        
    }
}