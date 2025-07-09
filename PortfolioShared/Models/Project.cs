using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioShared.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public List<string> Technologies { get; set; } = new List<string>();
        public string GitHubUrl { get; set; } = string.Empty; // GitHub repository URL
        public string GitHubProjectTitle { get; set; } = string.Empty; // GitHub Project title
        public string GitHubProjectColumn { get; set; } = string.Empty; // GitHub Project column name
        public int GitHubProjectNumber { get; set; } // GitHub Project number
        public int UserId { get; set; } // Foreign key to User
        public User? User { get; set; } // Navigation property
    }
}
