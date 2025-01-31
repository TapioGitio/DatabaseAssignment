using Business.Models.RegForms;
using Business.Models.SafeToDisplay;
using Business.Models.UpdateForms;

namespace Business.Interfaces
{
    public interface IServiceService
    {
        Task<bool> CreateServiceAsync(ServiceRegistrationForm form);
        Task<IEnumerable<Service>> ReadServicesAsync();

        Task<bool> UpdateServiceAsync(int id, ServiceUpdateForm form);
        Task<bool> DeleteServiceAsync(int id);
    }

}