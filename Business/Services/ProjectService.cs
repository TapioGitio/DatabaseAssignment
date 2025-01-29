using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
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
        var entity = await _projectRepository.ReadDetailedAsync(id);

        if (entity == null)
            return null!;

        var converted = ProjectFactory.CreateMajor(entity);

        return converted;
    }

    public async Task<bool> UpdateProjectAsync(int id, ProjectUpdateForm form)
    {

        var entity = await _projectRepository.ReadAsync(x => x.Id == id);

        if (entity == null)
            return false;

        var updatedEntity = ProjectFactory.Update(entity, form);

        var result = await _projectRepository.UpdateAsync(x => x.Id == id, updatedEntity);
        return result;
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        var question = await _projectRepository.DoesItExistAsync(x => x.Id == id);

        if (question == false)
            return false;

        return await _projectRepository.DeleteAsync(x => x.Id == id);
    }
}
