using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs;
using Core.Entites;

namespace Core.Interfaces
{
    public interface IBikeRepository
    {
        Task<IReadOnlyList<Bikes>> GetBikesAsync();
        Task<Bikes> GetBikesByIdAsync(int id);
         Task<bool> UpdateBikeAsync(Bikes updatedBike);
        Task<bool> DeleteBikeAsync(int id, string bikeName);
        Task<bool> InsertOrUpdateBikeAsync(Bikes bike);
    }
}