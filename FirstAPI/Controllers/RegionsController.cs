using FirstAPI.Data;
using FirstAPI.Interfaces;
using FirstAPI.Models.Domain;
using FirstAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly MyDbContext _dbContext;
        private readonly IRegionRepository _regionRepository;

        public RegionsController(MyDbContext dbContext, IRegionRepository regionRepository)
        {
            _dbContext = dbContext;
            _regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var regions = await _regionRepository.GetAll();
                var regionDTO = regions.Select(regionDomain => new RegionDTO
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                }).ToList();

                return Ok(regionDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomain = await _regionRepository.GetById(id);

            if (regionDomain == null)
                return NotFound();

            var regionDTO = new RegionDTO
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionDTO);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequest)
        {
            var regionDTO = _regionRepository.CreateRegion(addRegionRequest);

            return CreatedAtAction(nameof(GetById), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpPut]
        public IActionResult Update(Guid id , [FromBody] AddRegionRequestDto addRegionRequest)
        {
            var regionDTO = _regionRepository.UpdateRegion(id , addRegionRequest);

            if(regionDTO != null)
                return CreatedAtAction(nameof(GetById), new { id = regionDTO.Id }, regionDTO);
            else
                return BadRequest();
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var isDeleted = await _regionRepository.DeleteRegion(id);

            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
