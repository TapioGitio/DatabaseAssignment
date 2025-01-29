using Business.Factories;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository)
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<bool> CreateProjectAsync(ProjectRegistrationForm form)
    {
        if (form == null)
            return false;

        var entity = ProjectFactory.Create(form);
        return await _projectRepository.CreateAsync(entity);
    }

    public async Task<IEnumerable<ProjectOverallView>> ReadAllWithoutDetailsAsync()
    {
        var entities = await _projectRepository.ReadAllAsync();
        var converted = entities.Select(ProjectFactory.Create);

        return converted;
    }

    public async Task<ProjectDetailedView> ReadOneDetailedAsync(int id)
    {
        var entities = await _projectRepository.ReadDetailedAsync(id);

        if (entities == null)
            return null!;

        var converted = ProjectFactory.CreateMajor(entities);

        return converted;
    }

    public async Task<bool> UpdateProjectAsync(int id, ProjectUpdateForm form)
    {

        var entity = await _projectRepository.ReadAsync(x => x.Id == id);

        if (entity == null)
            return false;

        var updatedEntity = ProjectFactory.Update(entity, form);

        return await _projectRepository.UpdateAsync(x => x.Id == id, updatedEntity);

    }
}
