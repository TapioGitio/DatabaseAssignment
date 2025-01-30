using Business.Factories;
using Business.Interfaces;
using Business.Models.RegForms;
using Business.Models.UpdateForms;
using Data.Interfaces;

namespace Business.Services;

public class ServiceService(IServiceRepository serviceRepository) : IServiceService
{
    private readonly IServiceRepository _serviceRepository = serviceRepository;

    public async Task<bool> CreateServiceAsync(ServiceRegistrationForm form)
    {
        if (form == null)
            return false;
        var entity = ServiceFactory.Create(form);

        return await _serviceRepository.CreateAsync(entity);
    }


    public async Task<bool> UpdateServiceAsync(int id, ServiceUpdateForm form)
    {
        var entity = await _serviceRepository.ReadAsync(x => x.Id == id);

        if (entity == null)
            return false;

        if (form == null)
            return false;

        var updatedEntity = ServiceFactory.Update(entity, form);
        var result = await _serviceRepository.UpdateAsync(x => x.Id == id, updatedEntity);
        return result;
    }
    public async Task<bool> DeleteServiceAsync(int id)
    {
        var search = await _serviceRepository.DoesItExistAsync(x => x.Id == id);

        if (search == false)
            return false;

        return await _serviceRepository.DeleteAsync(x => x.Id == id);
    }
}
