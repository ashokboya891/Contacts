using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    public class BikesController:BaseApiController
    {
        public BikesDbContext _BikesDbContext ;
        public BikesController(BikesDbContext bikesDbContext)
        {
        _BikesDbContext = bikesDbContext;

        }
        [HttpGet("GetBikes")]
        public ActionResult<List<Bikes>> GetBikes()
        {
          var data= _BikesDbContext.tblBikes.ToList();
          return data;
          
        }
          [HttpGet("GetBikes/{id}")]
        public async Task<ActionResult<Bikes>> GetBikesById(int id)
        {
          return await _BikesDbContext.tblBikes.FindAsync(id);
          
          
        }

    }
}