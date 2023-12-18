using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs;
using Core.Entites;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Data
{
    public class BikesRepository : IBikeRepository
    {
        private readonly BikesDbContext _bikesDbContext;
        public BikesRepository(BikesDbContext bikesDbContext)
        {
            _bikesDbContext = bikesDbContext;

        }
        // public async Task<Bikes> AddBikeAsync()
        // {

        // }

        public async Task<IReadOnlyList<Bikes>> GetBikesAsync()
        {
            var data= await _bikesDbContext.tblBikes.ToListAsync();
            return data;
        }

        public async Task<Bikes> GetBikesByIdAsync(int id)
        {
           var data=await _bikesDbContext.tblBikes.FindAsync(id);
           return data;
        }
        public async Task<bool> UpdateBikeAsync(Bikes updatedBike)
        {
            var existingBike = await _bikesDbContext.tblBikes.FindAsync(updatedBike.Id);

            if (existingBike == null)
            {
                // Bike with the specified ID not found
                return false;
            }

            // Update the properties of the existing bike entity with the values from updatedBike
            _bikesDbContext.Entry(existingBike).CurrentValues.SetValues(updatedBike);

            await _bikesDbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteBikeAsync(int id, string bikeName)
        {
            var bikeToDelete = await _bikesDbContext.tblBikes
                .FirstOrDefaultAsync(b => b.Id == id && b.BikeName == bikeName);

            if (bikeToDelete == null)
            {
                return false;
            }

            _bikesDbContext.tblBikes.Remove(bikeToDelete);
            await _bikesDbContext.SaveChangesAsync();

            return true;
        }
      public async Task<bool> InsertOrUpdateBikeAsync(Bikes bike)
{
    // Check if a bike with the same BikeName and Model already exists
    var existingBike = await _bikesDbContext.tblBikes
        .FirstOrDefaultAsync(b => b.Model == bike.Model);

    if (existingBike != null)
    {
        // Bike with the same BikeName and Model already exists, don't save
        return false;
    }

    // Bike with the same BikeName and Model doesn't exist, add data
    _bikesDbContext.tblBikes.Add(bike);
    await _bikesDbContext.SaveChangesAsync();

    return true;
}






    }
}