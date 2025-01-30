using Business.Models.RegForms;
using Business.Models.UpdateForms;

namespace Business.Interfaces
{
    public interface IStatusService
    {
        Task<bool> CreateStatusAsync(StatusRegistrationForm form);
        Task<bool> UpdateStatusAsync(int id, StatusUpdateForm form);
        Task<bool> DeleteStatusAsync(int id);
    }

}