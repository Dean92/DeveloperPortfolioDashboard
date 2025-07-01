using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;
using PortfolioShared.Models;
using PortfolioShared.Dtos;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly PortfolioDbContext _context;
        public SkillsController(PortfolioDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Skill>> CreateSkill(SkillCreateDto skillDto)
        {
            var skill = new Skill
            {
                Name = skillDto.Name,
                UserId = skillDto.UserId
            };

            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetSkill), new { id = skill.Id }, skill);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> GetSkill(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
                return NotFound();
            return skill;
        }
    }
}
