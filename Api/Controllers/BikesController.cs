using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Api.DTOs;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace Api.Controllers
{
    public class BikesController:BaseApiController
    {
    private readonly IBikeRepository _bikeRepository;
        private readonly IMapper _mapper;
    public BikesController(IBikeRepository bikeRepository,IMapper mapper)
    {
      _mapper = mapper;
      _bikeRepository = bikeRepository;

    }
    [HttpGet]
    public string Get()
    {
      return "ashok";
    }
    // [Authorize]
    [HttpGet("AllBikes")]
    public async Task<ActionResult<IReadOnlyList<BikeDTo>>> GetBikes()
    {
      var bikes = await _bikeRepository.GetBikesAsync();

      if (bikes == null || bikes.Count == 0)
      {
        return NotFound("No bikes found");
      }
     return _mapper.Map<IReadOnlyList<Bikes>, List<BikeDTo>>(bikes);   
     //mapping helps when we have more things in class and you dont want to show unessary things that you will crate data transfwer object and
     // mapping profile to map data which is in class to dto so that matched data only receiced to end user through dto 
    //here above mapping no difference in bikes,bikesdto but difference will apear when we have more things in bikes and less rthings bikessto
      // return _mapper.Map<Bikes,BikeDTo>(bikes);
    }
 [HttpPut("UpdateBike/{id}")]
    public async Task<ActionResult> UpdateBike( [FromBody] Bikes updatedBike)
    {
      if ( updatedBike.Id==0)
      {
        // The provided ID in the URL doesn't match the ID in the request body
        return BadRequest("Incorrect id");
      }

      var isUpdated = await _bikeRepository.UpdateBikeAsync(updatedBike);

      if (!isUpdated)
      {
        // Bike with the specified ID not found
        return NotFound($"Bike with ID {updatedBike.Id} not found");
      }

      return NoContent(); // 204 No Content
    }
    [HttpDelete("DeleteBike/{id}/{bikeName}")]
    public async Task<ActionResult> DeleteBike(int id, string bikeName)
    {
      var isDeleted = await _bikeRepository.DeleteBikeAsync(id, bikeName);

      if (!isDeleted)
      {
        // Bike with the specified ID and BikeName not found
        return NotFound($"Bike with ID {id} and BikeName {bikeName} not found");
      }

      return NoContent(); // 204 No Content
    }
    [HttpPost("InsertOrUpdateBike")]
    public async Task<ActionResult> InsertOrUpdateBike([FromBody] Bikes bike)
    {
      var isInsertedOrUpdated = await _bikeRepository.InsertOrUpdateBikeAsync(bike);

      if (!isInsertedOrUpdated)
      {
        // Bike with the same BikeName and Model already exists
        return Conflict("Bike with the same BikeName and Model already exists");
      }

      return CreatedAtAction("GetBikes", new { id = bike.Id });
      // 201 Created with a link to the newly created or updated resource
    }





  }
}