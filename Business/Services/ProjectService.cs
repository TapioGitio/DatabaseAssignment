using Business.Factories;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepo)
{
    private readonly IProjectRepository _projectRepo = projectRepo;

    public async Task<bool> CreateProjectAsync(ProjectRegistrationForm form) 
    {
        if (form == null)
            return false;

        var entity = ProjectFactory.Create(form);
        return await _projectRepo.CreateAsync(entity);
    }

    public async Task<IEnumerable<ProjectOverallView>> ReadWithoutDetails()
    {
        var entities = await _projectRepo.ReadAllAsync();
        var converted = entities.Select(e => ProjectFactory.CreateMinor(e));

        return converted.ToList();
    }

    public async Task<IEnumerable<ProjectDetailedView>> ReadDetailedInfo(int id)
    {
        var entities = await _projectRepo.ReadDetailedAsync(id);
       

        return converted.ToList();
    }
}
