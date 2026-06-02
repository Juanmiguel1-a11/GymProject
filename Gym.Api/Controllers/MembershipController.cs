using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Gym.Api.DTOs.Request;
using Gym.Api.DTOs.Response;
using Gym.Domain.Entities;
using Gym.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembershipController : ControllerBase
    {
        private readonly IMembershipService _service;
        private readonly IMapper _mapper;

        public MembershipController(IMembershipService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MembershipResponse>>> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<MembershipResponse>>(items));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MembershipResponse>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(_mapper.Map<MembershipResponse>(item));
        }

        [HttpPost]
        public async Task<ActionResult<MembershipResponse>> Create(CreateMembershipRequest request)
        {
            try
            {
                var entity = _mapper.Map<Membership>(request);
                entity.IsActive = true; // Set as active by default
                var created = await _service.CreateAsync(entity);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<MembershipResponse>(created));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (System.Collections.Generic.KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
