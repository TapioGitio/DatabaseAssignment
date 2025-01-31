using Business.Models.RegForms;
using Business.Models.SafeToDisplay;
using Business.Models.UpdateForms;

namespace Business.Interfaces
{
    public interface IProjectManagerService
    {
        Task<bool> CreatePMAsync(ProjectManagerRegistrationForm form);
        Task<IEnumerable<ProjectManager>> ReadPMAsync();

        Task<bool> UpdatePMAsync(int id, ProjectManagerUpdateForm form);
        Task<bool> DeletePMAsync(int id);
    }

}