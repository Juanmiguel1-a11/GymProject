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
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _service;
        private readonly IMapper _mapper;

        public EnrollmentController(IEnrollmentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrollmentResponse>>> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<EnrollmentResponse>>(items));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EnrollmentResponse>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(_mapper.Map<EnrollmentResponse>(item));
        }

        [HttpPost]
        public async Task<ActionResult<EnrollmentResponse>> Create(CreateEnrollmentRequest request)
        {
            try
            {
                var entity = _mapper.Map<Enrollment>(request);
                var created = await _service.CreateAsync(entity);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<EnrollmentResponse>(created));
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
