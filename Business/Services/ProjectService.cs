using Data.Interfaces;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepo)
{
    private readonly IProjectRepository _projectRepo = projectRepo;

    public async Task CreateProjectAsync() 
    {
        
    }
}
