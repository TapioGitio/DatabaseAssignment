using Business.Models;
using Business.Models.RegForms;
using Business.Models.UpdateForms;

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

    public interface ICustomerService
    {
        Task<bool> CreateProjectAsync(CustomerRegistrationForm form);
        Task<bool> UpdateProjectAsync(int id, CustomerUpdateForm form);
        Task<bool> DeleteProjectAsync(int id);
    }

    public interface IServiceService
    {
        Task<bool> CreateProjectAsync(ProjectRegistrationForm form);
        Task<IEnumerable<ProjectOverallView>> ReadAllWithoutDetailsAsync();
        Task<ProjectDetailedView> ReadOneDetailedAsync(int id);
        Task<bool> UpdateProjectAsync(int id, ProjectUpdateForm form);
        Task<bool> DeleteProjectAsync(int id);
    }

    public interface IProjectManagerService
    {
        Task<bool> CreateProjectAsync(ProjectRegistrationForm form);
        Task<IEnumerable<ProjectOverallView>> ReadAllWithoutDetailsAsync();
        Task<ProjectDetailedView> ReadOneDetailedAsync(int id);
        Task<bool> UpdateProjectAsync(int id, ProjectUpdateForm form);
        Task<bool> DeleteProjectAsync(int id);
    }

    public interface IStatusService
    {
        Task<bool> CreateProjectAsync(ProjectRegistrationForm form);
        Task<IEnumerable<ProjectOverallView>> ReadAllWithoutDetailsAsync();
        Task<ProjectDetailedView> ReadOneDetailedAsync(int id);
        Task<bool> UpdateProjectAsync(int id, ProjectUpdateForm form);
        Task<bool> DeleteProjectAsync(int id);
    }

}