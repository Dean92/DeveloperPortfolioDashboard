using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioShared.Dtos
{
    public class ProjectCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public List<string> Technologies { get; set; } = new List<string>();
        public string GitHubRepoUrl { get; set; } = string.Empty;
        public int GitHubProjectNumber { get; set; }
        public string GitHubProjectTitle { get; set; } = string.Empty;
        public string GitHubProjectColumn { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}
