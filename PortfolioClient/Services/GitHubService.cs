using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace PortfolioClient.Services
{
    public class GitHubService
    {
        private readonly GraphQLHttpClient _graphQLClient;
        private readonly IConfiguration _configuration;

        public GitHubService(IConfiguration configuration)
        {
            _configuration = configuration;
            _graphQLClient = new GraphQLHttpClient(
                new GraphQLHttpClientOptions { EndPoint = new Uri("https://api.github.com/graphql") },
                new NewtonsoftJsonSerializer());
            var token = _configuration["GitHub:AccessToken"];
            if (!string.IsNullOrEmpty(token))
            {
                _graphQLClient.HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                _graphQLClient.HttpClient.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("PortfolioDashboard", "1.0"));
            }
        }

        public async Task<(string Title, string ColumnName)?> GetGitHubProjectDataAsync(string gitHubUrl, int projectNumber)
        {
            try
            {
                // Extract owner and repo from URL (e.g., https://github.com/username/repo -> username/repo)
                var uri = new Uri(gitHubUrl);
                var pathSegments = uri.AbsolutePath.Trim('/').Split('/');
                if (pathSegments.Length < 2) return null;
                var owner = pathSegments[0];
                var repo = pathSegments[1];

                var query = new GraphQLRequest
                {
                    Query = @"
                    query($owner: String!, $repo: String!, $projectNumber: Int!) {
                        repository(owner: $owner, name: $repo) {
                            projectV2(number: $projectNumber) {
                                title
                                items(first: 1) {
                                    nodes {
                                        fieldValues(first: 1) {
                                            nodes {
                                                ... on ProjectV2ItemFieldSingleSelectValue {
                                                    field {
                                                        ... on ProjectV2SingleSelectField {
                                                            name
                                                        }
                                                    }
                                                    optionId
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }",
                    Variables = new { owner, repo, projectNumber }
                };

                var response = await _graphQLClient.SendQueryAsync<GitHubProjectResponse>(query);
                if (response.Errors != null && response.Errors.Any())
                {
                    Console.WriteLine($"GraphQL errors for {gitHubUrl}, project {projectNumber}: {string.Join(", ", response.Errors.Select(e => e.Message))}");
                    return null;
                }

                var project = response.Data?.Repository?.ProjectV2;
                if (project == null) return null;

                var columnName = project.Items?.Nodes?.FirstOrDefault()?.FieldValues?.Nodes?.FirstOrDefault()?.Field?.Name ?? "Unknown";
                return (project.Title, columnName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching GitHub project data for {gitHubUrl}, project {projectNumber}: {ex.Message}");
                return null;
            }
        }
    }

    public class GitHubProjectResponse
    {
        public Repository Repository { get; set; }
    }

    public class Repository
    {
        public ProjectV2 ProjectV2 { get; set; }
    }

    public class ProjectV2
    {
        public string Title { get; set; }
        public ProjectItems Items { get; set; }
    }

    public class ProjectItems
    {
        public List<ProjectItemNode> Nodes { get; set; }
    }

    public class ProjectItemNode
    {
        public FieldValues FieldValues { get; set; }
    }

    public class FieldValues
    {
        public List<FieldValueNode> Nodes { get; set; }
    }

    public class FieldValueNode
    {
        public ProjectField Field { get; set; }
        public string OptionId { get; set; }
    }

    public class ProjectField
    {
        public string Name { get; set; }
    }
}

