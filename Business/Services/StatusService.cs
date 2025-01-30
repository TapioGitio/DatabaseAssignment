using Business.Factories;
using Business.Interfaces;
using Business.Models.RegForms;
using Business.Models.UpdateForms;
using Data.Interfaces;

namespace Business.Services;

public class StatusService(IStatusRepository statusRepository) : IStatusService
{
    private readonly IStatusRepository _statusRepository = statusRepository;

    public async Task<bool> CreateStatusAsync(StatusRegistrationForm form)
    {
        if (form == null)
            return false;
        var entity = StatusFactory.Create(form);

        return await _statusRepository.CreateAsync(entity);
    }


    public async Task<bool> UpdateStatusAsync(int id, StatusUpdateForm form)
    {
        var entity = await _statusRepository.ReadAsync(x => x.Id == id);

        if (entity == null)
            return false;

        if (form == null)
            return false;

        var updatedEntity = StatusFactory.Update(entity, form);
        var result = await _statusRepository.UpdateAsync(x => x.Id == id, updatedEntity);
        return result;
    }
    public async Task<bool> DeleteStatusAsync(int id)
    {
        var search = await _statusRepository.DoesItExistAsync(x => x.Id == id);

        if (search == false)
            return false;

        return await _statusRepository.DeleteAsync(x => x.Id == id);
    }
}
