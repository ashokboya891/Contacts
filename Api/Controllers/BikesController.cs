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
using Api.Extensions;
using Api.services;

namespace Api.Controllers
{
  public class BikesController : BaseApiController
  {
    private readonly IBikeRepository _bikeRepository;
    private readonly IMapper _mapper;
    private readonly PaginationService<Bikes> _paginationService;

    public BikesController(IBikeRepository bikeRepository, IMapper mapper, PaginationService<Bikes> paginationService)
    {
      _bikeRepository = bikeRepository;
      _mapper = mapper;
      _paginationService = paginationService;
    }
    [HttpGet]
    public string Get()
    {
      return "ashok";
    }
    // [Authorize]
    // [HttpGet("AllBikes")]
    // public async Task<ActionResult<IReadOnlyList<BikeDTo>>> GetBikes()
    // {
    //   var bikes = await _bikeRepository.GetBikesAsync();

    //   if (bikes == null || bikes.Count == 0)
    //   {
    //     return NotFound("No bikes found");
    //   }
    //  return _mapper.Map<IReadOnlyList<Bikes>, List<BikeDTo>>(bikes);   
    //mapping helps when we have more things in class and you dont want to show unessary things that you will crate data transfwer object and
    // mapping profile to map data which is in class to dto so that matched data only receiced to end user through dto 
    //here above mapping no difference in bikes,bikesdto but difference will apear when we have more things in bikes and less rthings bikessto
    // return _mapper.Map<Bikes,BikeDTo>(bikes);
    // }

    // [HttpGet("AllBikes")]
    // public async Task<ActionResult<PagedResult<BikeDTo>>> GetBikes(int page = 1, int pageSize = 3)
    // {
    //     var bikes = await _bikeRepository.GetBikesAsync();

    //     if (bikes == null || bikes.Count == 0)
    //     {
    //         return NotFound("No bikes found");
    //     }

    //     var totalItems = bikes.Count;
    //     var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

    //     var bikesPage = bikes.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    //     var bikesDto = _mapper.Map<List<BikeDTo>>(bikesPage);

    //     var result = new PagedResult<BikeDTo>
    //     {
    //         CurrentPage = page,
    //         PageSize = pageSize,
    //         TotalItems = totalItems,
    //         TotalPages = totalPages,
    //         Items = bikesDto
    //     };

    //     return result;
    // }

    [HttpGet("AllBikes")]
    public async Task<ActionResult<PagedResult<BikeDTo>>> GetBikes(int page = 1, int pageSize = 3)
    {
      var bikes = await _bikeRepository.GetBikesAsync();

      if (bikes == null || bikes.Count == 0)
      {
        return NotFound("No bikes found");
      }

      var bikesList = bikes.ToList();

      if (_paginationService != null)
      {
        // Ensure _paginationService is not null before using it
        var result = _paginationService.Paginate<BikeDTo>(bikesList, page, pageSize);

        // Add pagination header to the response
        Response.AddPaginationHeader(_paginationService.GetPaginationHeader(result));

        return result;
      }
      else
      {
        // Log or handle the case where _paginationService is null
        return StatusCode(500, "Pagination service is not properly initialized.");
      }
    }

    [HttpPut("UpdateBike/{id}")]
    public async Task<ActionResult> UpdateBike([FromBody] Bikes updatedBike)
    {
      if (updatedBike.Id == 0)
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