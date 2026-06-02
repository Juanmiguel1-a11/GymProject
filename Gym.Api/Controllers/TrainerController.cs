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
    public class TrainersController : ControllerBase
    {
        private readonly ITrainerService _trainerService;
        private readonly IMapper _mapper;

        public TrainersController(ITrainerService trainerService, IMapper mapper)
        {
            _trainerService = trainerService;
            _mapper = mapper;
        }

        // GET api/trainers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainerResponse>>> GetAll()
        {
            var trainers = await _trainerService.GetAllTrainersAsync();
            return Ok(_mapper.Map<IEnumerable<TrainerResponse>>(trainers));
        }

        // GET api/trainers/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TrainerResponse>> GetById(int id)
        {
            var trainer = await _trainerService.GetTrainerByIdAsync(id);
            if (trainer is null) return NotFound($"Entrenador con Id {id} no encontrado.");
            return Ok(_mapper.Map<TrainerResponse>(trainer));
        }

        // GET api/trainers/active
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<TrainerResponse>>> GetActive()
        {
            var trainers = await _trainerService.GetActiveTrainersAsync();
            return Ok(_mapper.Map<IEnumerable<TrainerResponse>>(trainers));
        }

        // GET api/trainers/specialization/2
        [HttpGet("specialization/{spec:int}")]
        public async Task<ActionResult<IEnumerable<TrainerResponse>>> GetBySpecialization(int spec)
        {
            var trainers = await _trainerService.GetTrainersBySpecializationAsync((Gym.Domain.Enums.TrainerSpecialization)spec);
            return Ok(_mapper.Map<IEnumerable<TrainerResponse>>(trainers));
        }

        // POST api/trainers
        [HttpPost]
        public async Task<ActionResult<TrainerResponse>> Create([FromBody] CreateTrainerRequest request)
        {
            try
            {
                var trainer = _mapper.Map<Trainer>(request);
                var created = await _trainerService.CreateTrainerAsync(trainer);
                var response = _mapper.Map<TrainerResponse>(created);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/trainers/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<TrainerResponse>> Update(int id, [FromBody] UpdateTrainerRequest request)
        {
            try
            {
                var trainer = _mapper.Map<Trainer>(request);
                trainer.Id = id;
                var updated = await _trainerService.UpdateTrainerAsync(trainer);
                return Ok(_mapper.Map<TrainerResponse>(updated));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/trainers/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _trainerService.DeleteTrainerAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PATCH api/trainers/5/toggle-status
        [HttpPatch("{id:int}/toggle-status")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            try
            {
                var isNowActive = await _trainerService.ToggleTrainerStatusAsync(id);
                return Ok(new { isActive = isNowActive, message = isNowActive ? "Entrenador activado." : "Entrenador desactivado." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}