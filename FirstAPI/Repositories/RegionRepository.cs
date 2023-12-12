using FirstAPI.Data;
using FirstAPI.Interfaces;
using FirstAPI.Models.Domain;
using FirstAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly MyDbContext _context;
        public RegionRepository(MyDbContext context)
        {
            _context = context;
        }

        public RegionDTO CreateRegion(AddRegionRequestDto addRegionRequest)
        {
            var regionDomainModel = new Region
            {
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                RegionImageUrl = addRegionRequest.RegionImageUrl,
            };

            _context.Regions.Add(regionDomainModel);
            _context.SaveChanges();

            var regionDTO = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return regionDTO;
        }

        //public bool DeleteRegion(Region region)
        //{
        //    try
        //    {
        //        _context.Remove(region);
        //        _context.SaveChanges();
        //        return true; 
        //    }
        //    catch (Exception ex)
        //    {                             
        //        return false; 
        //    }
        //}

        public async Task<bool> DeleteRegion(Guid id)
        {
            var regionToDelete = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionToDelete == null)
                return false;

            _context.Regions.Remove(regionToDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Region>> GetAll()
        {
            var regionsDomain = await _context.Regions.ToListAsync();
            return regionsDomain;
        }

        public async Task<Region> GetById(Guid id)
        {
            var regionDomain = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            return regionDomain;
        }
        public bool Save()
        {

            try
            {
                _context.SaveChanges();
                return true; // or return some meaningful value indicating success
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return false; // or return some meaningful value indicating failure
            }

        }
        
        [HttpPut]
        public RegionDTO UpdateRegion(Guid regionId, AddRegionRequestDto addRegionRequest)
        {
            var existingRegion = _context.Regions.FirstOrDefault(r => r.Id == regionId);

            if (existingRegion != null)
            {
                existingRegion.Code = addRegionRequest.Code;
                existingRegion.Name = addRegionRequest.Name;
                existingRegion.RegionImageUrl = addRegionRequest.RegionImageUrl;

                _context.SaveChanges();

                var regionDTO = new RegionDTO
                {
                    Id = existingRegion.Id,
                    Code = existingRegion.Code,
                    Name = existingRegion.Name,
                    RegionImageUrl = existingRegion.RegionImageUrl
                };

                return regionDTO;
            }
            else
            {
                // Handle the case where the region with the specified ID was not found.
                // You might return a NotFound response or take some other appropriate action.
                return null;
            }
        }
    }
}
