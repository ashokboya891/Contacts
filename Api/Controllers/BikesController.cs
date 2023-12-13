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

namespace Api.Controllers
{
    public class BikesController:BaseApiController
    {
    private readonly IBikeRepository _bikeRepository;
    public BikesController(IBikeRepository bikeRepository)
    {
      _bikeRepository = bikeRepository;

    }
    [HttpGet]
    public string Get()
    {
      return "ashok";
    }
    [HttpGet("AllBikes")]
    public async Task<ActionResult<IReadOnlyList<Bikes>>> GetBikes()
    {
      var bikes = await _bikeRepository.GetBikesAsync();

      if (bikes == null || bikes.Count == 0)
      {
        return NotFound("No bikes found");
      }

      return Ok(bikes);
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