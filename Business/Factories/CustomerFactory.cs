using Business.Models.RegForms;
using Business.Models.UpdateForms;
using Data.Entities;

namespace Business.Factories;

public static class CustomerFactory
{
    public static CustomerRegistrationForm Create() => new();

    public static CustomerEntity Create(CustomerRegistrationForm form)
    {
        return new CustomerEntity
        {
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email,
        };
    }

    public static CustomerEntity Update(CustomerEntity entity, CustomerUpdateForm form)
    {
        return new CustomerEntity
        {
            Id = entity.Id,
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email,
        };
    }
}
