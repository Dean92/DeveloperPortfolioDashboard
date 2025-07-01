using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioShared.Dtos
{
    public class ProjectCreateDto
    {
        /// <summary>
        /// The title of the project.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// A description of the project.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// The GitHub URL of the project.
        /// </summary>
        public string GitHubUrl { get; set; } = string.Empty;

        /// <summary>
        /// The number of GitHub stars for the project.
        /// </summary>
        public int Stars { get; set; }

        /// <summary>
        /// A list of technologies used in the project.
        /// </summary>
        public List<string> Technologies { get; set; } = new();

        /// <summary>
        /// The ID of the user who owns the project.
        /// </summary>
        public int UserId { get; set; }
    }
}
