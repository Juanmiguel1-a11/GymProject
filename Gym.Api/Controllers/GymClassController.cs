using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Gym.Domain.Entities;
using Gym.Domain.Interfaces.Services;
using Gym.Api.DTOs.Request;
using Gym.Api.DTOs.Response;

namespace Gym.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GymClassController : ControllerBase
    {
        private readonly IGymClassService _service;
        private readonly IMapper _mapper;

        public GymClassController(IGymClassService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GymClassResponseDto>>> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<GymClassResponseDto>>(items));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GymClassResponseDto>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(_mapper.Map<GymClassResponseDto>(item));
        }

        [HttpPost]
        public async Task<ActionResult<GymClassResponseDto>> Create(GymClassRequestDto request)
        {
            var entity = _mapper.Map<GymClass>(request);
            var created = await _service.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<GymClassResponseDto>(created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GymClassRequestDto request)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(request, existing);
            await _service.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
