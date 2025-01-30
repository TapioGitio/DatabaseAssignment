using Business.Models.RegForms;
using Business.Models.UpdateForms;

namespace Business.Interfaces
{
    public interface IProjectManagerService
    {
        Task<bool> CreatePMAsync(ProjectManagerRegistrationForm form);
        Task<bool> UpdatePMAsync(int id, ProjectManagerUpdateForm form);
        Task<bool> DeletePMAsync(int id);
    }

}