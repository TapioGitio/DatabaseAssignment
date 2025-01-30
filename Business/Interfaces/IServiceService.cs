using Business.Models.RegForms;
using Business.Models.UpdateForms;

namespace Business.Interfaces
{
    public interface IServiceService
    {
        Task<bool> CreateServiceAsync(ServiceRegistrationForm form);
        Task<bool> UpdateServiceAsync(int id, ServiceUpdateForm form);
        Task<bool> DeleteServiceAsync(int id);
    }

}