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
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;

        public MembersController(IMemberService memberService, IMapper mapper)
        {
            _memberService = memberService;
            _mapper = mapper;
        }

        // GET api/members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberResponse>>> GetAll()
        {
            var members = await _memberService.GetAllMembersAsync();
            return Ok(_mapper.Map<IEnumerable<MemberResponse>>(members));
        }

        // GET api/members/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MemberResponse>> GetById(int id)
        {
            var member = await _memberService.GetMemberByIdAsync(id);
            if (member is null) return NotFound($"Miembro con Id {id} no encontrado.");
            return Ok(_mapper.Map<MemberResponse>(member));
        }

        // GET api/members/email/john@gym.com
        [HttpGet("email/{email}")]
        public async Task<ActionResult<MemberResponse>> GetByEmail(string email)
        {
            var member = await _memberService.GetMemberByEmailAsync(email);
            if (member is null) return NotFound($"No se encontró miembro con email '{email}'.");
            return Ok(_mapper.Map<MemberResponse>(member));
        }

        // GET api/members/status/1
        [HttpGet("status/{status:int}")]
        public async Task<ActionResult<IEnumerable<MemberResponse>>> GetByStatus(int status)
        {
            var members = await _memberService.GetMembersByStatusAsync((Gym.Domain.Enums.MemberStatus)status);
            return Ok(_mapper.Map<IEnumerable<MemberResponse>>(members));
        }

        // GET api/members/membership/1
        [HttpGet("membership/{type:int}")]
        public async Task<ActionResult<IEnumerable<MemberResponse>>> GetByMembership(int type)
        {
            var members = await _memberService.GetMembersByMembershipTypeAsync((Gym.Domain.Enums.MembershipType)type);
            return Ok(_mapper.Map<IEnumerable<MemberResponse>>(members));
        }

        // POST api/members
        [HttpPost]
        public async Task<ActionResult<MemberResponse>> Create([FromBody] CreateMemberRequest request)
        {
            try
            {
                var member = _mapper.Map<Member>(request);
                var created = await _memberService.CreateMemberAsync(member);
                var response = _mapper.Map<MemberResponse>(created);
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

        // PUT api/members/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<MemberResponse>> Update(int id, [FromBody] UpdateMemberRequest request)
        {
            try
            {
                var member = _mapper.Map<Member>(request);
                member.Id = id;
                var updated = await _memberService.UpdateMemberAsync(member);
                return Ok(_mapper.Map<MemberResponse>(updated));
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

        // DELETE api/members/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _memberService.DeleteMemberAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PATCH api/members/5/status
        [HttpPatch("{id:int}/status")]
        public async Task<IActionResult> ChangeStatus(int id, [FromBody] ChangeMemberStatusRequest request)
        {
            try
            {
                await _memberService.ChangeMemberStatusAsync(id, request.NewStatus);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        // PATCH api/members/5/membership
        [HttpPatch("{id:int}/membership")]
        public async Task<IActionResult> UpgradeMembership(int id, [FromBody] UpgradeMembershipRequest request)
        {
            try
            {
                await _memberService.UpgradeMembershipAsync(id, request.NewMembershipType);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}