using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioShared.Dtos
{
    public class SkillCreateDto
    {
        /// <summary>
        /// The name of the skill.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The ID of the user who owns the skill.
        /// </summary>
        public int UserId { get; set; }
    }
}
