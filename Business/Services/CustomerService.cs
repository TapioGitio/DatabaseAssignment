using Business.Factories;
using Business.Interfaces;
using Business.Models.RegForms;
using Business.Models.UpdateForms;
using Data.Interfaces;

namespace Business.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<bool> CreateCustomerAsync(CustomerRegistrationForm form)
    {
        if (form == null)
            return false;

        var entity = CustomerFactory.Create(form);

        return await _customerRepository.CreateAsync(entity);
    }


    public async Task<bool> UpdateCustomerAsync(int id, CustomerUpdateForm form)
    {
        var entity = await _customerRepository.ReadAsync(x => x.Id == id);

        if (entity == null)
            return false;

        if (form == null)
            return false;

        var updatedEntity = CustomerFactory.Update(entity, form);
        var result = await _customerRepository.UpdateAsync(x => x.Id == id, updatedEntity);
        return result;
    }
    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var search = await _customerRepository.DoesItExistAsync(x => x.Id == id);

        if (search == false)
            return false;

        return await _customerRepository.DeleteAsync(x => x.Id == id);
    }
}
