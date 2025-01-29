using Business.Models;

namespace Business.Interfaces
{
    public interface IProjectService
    {
        Task<bool> CreateProjectAsync(ProjectRegistrationForm form);
        Task<IEnumerable<ProjectOverallView>> ReadAllWithoutDetailsAsync();
        Task<ProjectDetailedView> ReadOneDetailedAsync(int id);
        Task<bool> UpdateProjectAsync(int id, ProjectUpdateForm form);
        Task<bool> DeleteProjectAsync(int id);
    }
}