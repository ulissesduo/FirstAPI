using FirstAPI.Models.Domain;
using FirstAPI.Models.DTO;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Interfaces
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAll();

        Task<Region> GetById(Guid id);

        //bool DeleteRegion(Region region);
        Task<bool> DeleteRegion(Guid id);

        bool Save();
        RegionDTO CreateRegion(AddRegionRequestDto addRegionRequest);
        RegionDTO UpdateRegion(Guid regionId, AddRegionRequestDto addRegionRequest);
    }
}
