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
        public string GitHubUrl { get; set; } = string.Empty;
        public int Stars { get; set; }
        public List<string> Technologies { get; set; } = new();
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
