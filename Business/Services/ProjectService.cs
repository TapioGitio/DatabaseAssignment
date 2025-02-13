using Business.Factories;
using Business.Interfaces;
using Business.Models.RegForms;
using Business.Models.SafeToDisplay;
using Business.Models.UpdateForms;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<bool> CreateProjectAsync(ProjectRegistrationForm form)
    {
        await _projectRepository.BeginTransactionAsync();

        try
        {
            if (form == null || string.IsNullOrWhiteSpace(form.Name))
                return false;

            var entity = ProjectFactory.Create(form);
            var result = await _projectRepository.CreateAsync(entity);

            await _projectRepository.CommitTransactionAsync();
            return result;
        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<IEnumerable<ProjectOverallView>> ReadAllWithoutDetailsAsync()
    {
        var entities = await _projectRepository.ReadAllAsync(query =>
            query.Include(status => status.Status));

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
        await _projectRepository.BeginTransactionAsync();

        try
        {
            var entity = await _projectRepository.ReadAsync(x => x.Id == id);

            if (entity == null || form == null)
                return false;

            var updatedEntity = ProjectFactory.Update(entity, form);
            var result = await _projectRepository.UpdateAsync(x => x.Id == id, updatedEntity);

            await _projectRepository.CommitTransactionAsync();
            return result;
        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Could not update the specific project: { ex.Message}");
            return false;
        }
    }

    public async Task<bool> ProjectDuplicateAsync(string projectName)
    {
        var search = await _projectRepository.DoesProjectExistAsync(projectName);
        if (search)
            return true;
        return false;
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {

        try
        {
            var question = await _projectRepository.DoesItExistAsync(x => x.Id == id);

            if (!question)
                return false;

            return await _projectRepository.DeleteAsync(x => x.Id == id);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Could not remove: {id}, {ex.Message}");
            return false;
        }
    }
}
