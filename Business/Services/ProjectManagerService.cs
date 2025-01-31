using Business.Factories;
using Business.Interfaces;
using Business.Models.RegForms;
using Business.Models.SafeToDisplay;
using Business.Models.UpdateForms;
using Data.Interfaces;

namespace Business.Services;

public class ProjectManagerService(IProjectManagerRepository projectManagerRepository) : IProjectManagerService
{
    private readonly IProjectManagerRepository _projectManagerRepository = projectManagerRepository;

    public async Task<bool> CreatePMAsync(ProjectManagerRegistrationForm form)
    {
        if (form == null)
            return false;

        var entity = ProjectManagerFactory.Create(form);

        return await _projectManagerRepository.CreateAsync(entity); 
    }
    public async Task<IEnumerable<ProjectManager>> ReadPMAsync()
    {
        var entities = await _projectManagerRepository.ReadAllAsync();

        var converted = entities.Select(ProjectManagerFactory.Create);

        return converted;
    }

    public async Task<bool> UpdatePMAsync(int id, ProjectManagerUpdateForm form)
    {
        var entity = await _projectManagerRepository.ReadAsync(x => x.Id == id);

        if (entity == null)
            return false;

        if (form == null)
            return false;

        var updatedEntity = ProjectManagerFactory.Update(entity, form);
        var result = await _projectManagerRepository.UpdateAsync(x => x.Id == id, updatedEntity);
        return result;
    }

    public async Task<bool> DeletePMAsync(int id)
    {
        var search = await _projectManagerRepository.DoesItExistAsync(x => x.Id == id);

        if (search == false)
            return false;
        
        return await _projectManagerRepository.DeleteAsync(x => x.Id == id);
    }

}
